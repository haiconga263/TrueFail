using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using DAL.Mappers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Web.Controllers;
using Web.Conventions;
using Web.Expanders;
using Web.Filters;
using Web.ModelBinders;
using Web.Providers;
using Web.Queries;
using Web.Repositories;

namespace Web.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadInstalledModules(this IServiceCollection services, string contentRootPath)
        {
            var modules = new List<ModuleInfo>();
            var moduleRootFolder = new DirectoryInfo(Path.Combine(contentRootPath, "Modules"));
            if (!moduleRootFolder.Exists)
            {
                GlobalConfiguration.Modules = modules;
                return services;
            }

            var moduleFolders = moduleRootFolder.GetDirectories();

            foreach (var moduleFolder in moduleFolders)
            {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }

                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException)
                    {
                        // Get loaded assembly
                        assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                        if (assembly == null)
                        {
                            throw;
                        }

                        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                        string loadedAssemblyVersion = fvi.FileVersion;

                        fvi = FileVersionInfo.GetVersionInfo(file.FullName);
                        string tryToLoadAssemblyVersion = fvi.FileVersion;

                        // Or log the exception somewhere and don't add the module to list so that it will not be initialized
                        if (tryToLoadAssemblyVersion != loadedAssemblyVersion)
                        {
                            throw new Exception($"Cannot load {file.FullName} {tryToLoadAssemblyVersion} because {assembly.Location} {loadedAssemblyVersion} has been loaded");
                        }
                    }

                    if (assembly.FullName.Contains(moduleFolder.Name + ","))
                    {
                        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                        modules.Add(new ModuleInfo
                        {
                            Name = moduleFolder.Name,
                            Assembly = assembly,
                            Path = moduleFolder.FullName,
                            Version = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion
                        });
                    }
                }
            }


            foreach (var module in modules)
            {
                var types = module.Assembly.GetTypes();
                var moduleInitializerType = module.Assembly.GetTypes().FirstOrDefault(x => typeof(IModuleInitializer).IsAssignableFrom(x));
                if ((moduleInitializerType != null) && (moduleInitializerType != typeof(IModuleInitializer)))
                {
                    services.AddSingleton(typeof(IModuleInitializer), moduleInitializerType);
                }
            }
            GlobalConfiguration.Modules = modules;
            return services;
        }

        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services, IList<ModuleInfo> modules)
        {
            var mvcBuilder = services
                .AddMvc(o =>
                {
                    o.ModelBinderProviders.Insert(0, new InvariantDecimalModelBinderProvider());
                    o.Filters.Add(typeof(GlobalExceptionFilter));
                    o.Filters.Add(typeof(SessionFilter));
                })
                .AddRazorOptions(o =>
                {
                    foreach (var module in modules)
                    {
                        o.AdditionalCompilationReferences.Add(MetadataReference.CreateFromFile(module.Assembly.Location));
                    }
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            foreach (var module in modules)
            {
                mvcBuilder.AddApplicationPart(module.Assembly);
            }

            return services;
        }


        public static IServiceProvider Build(this IServiceCollection services,
            IConfiguration configuration, IHostingEnvironment hostingEnvironment, bool IsCustomizedMvc = true)
        {

            GlobalConfiguration.Load(configuration);

            services.LoadInstalledModules(hostingEnvironment.ContentRootPath);

            if (IsCustomizedMvc)
                services.AddCustomizedMvc(GlobalConfiguration.Modules);

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            //Config logger log4net:
            var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo(GlobalConfiguration.Log4netFilePath));

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });


            services.BuildIntergration(hostingEnvironment);

            TypeMapper.Initialize(GlobalConfiguration.Modules.Where(m => m.Name.EndsWith(".UI")).Select(m => m.Assembly));
            TypeMapper.Initialize(Assembly.GetAssembly(typeof(GlobalConfiguration)));

            var builder = new ContainerBuilder();

            foreach (var module in GlobalConfiguration.Modules)
            {
                builder.RegisterAssemblyTypes(module.Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module.Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module.Assembly).Where(t => t.Name.EndsWith("Handler")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module.Assembly).Where(t => t.Name.EndsWith("Provider")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module.Assembly).Where(t => t.Name.EndsWith("Context")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module.Assembly).Where(t => t.Name.EndsWith("Helper")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module.Assembly).Where(t => t.Name.EndsWith("Queries")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module.Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            }
            foreach (var module in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("Common")))
            {
                builder.RegisterAssemblyTypes(module).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module).Where(t => t.Name.EndsWith("Handler")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module).Where(t => t.Name.EndsWith("Provider")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module).Where(t => t.Name.EndsWith("Context")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module).Where(t => t.Name.EndsWith("Helper")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module).Where(t => t.Name.EndsWith("Queries")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(module).AsClosedTypesOf(typeof(IRequestHandler<,>));
            }

            // MediatR
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            // Required by MediatR - for Command/CommandHandler
            builder.Register<SingleInstanceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return t => componentContext.TryResolve(t, out var o) ? o : null;
            });

            // Required by MediatR - for DomainEvent/DomainEventHandler
            builder.Register<MultiInstanceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return t =>
                {
                    var resolved = (IEnumerable<object>)componentContext.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                    return resolved;
                };
            });

            builder.RegisterInstance(configuration);
            builder.RegisterInstance(hostingEnvironment);
            builder.Populate(services);

            var container = builder.Build();
            GlobalConfiguration.Container = container;


            return container.Resolve<IServiceProvider>();
        }


        public static void BuildIntergration(this IServiceCollection services, IHostingEnvironment environment)
        {

            GlobalConfiguration.IntegrationTemplate = LoadInteConfigHelper.Load(GlobalConfiguration.IntegrationConfigFileName);
            if (GlobalConfiguration.IntegrationTemplate != null)
            {
                services.AddMvc(o => o.Conventions.Add(new GenericControllerRouteConvention())).ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));

                // register more type of command/commandhandler and repository genaric
                foreach (var model in GlobalConfiguration.IntegrationTemplate.ModelTemplates)
                {
                    // add genaric mediatR
                    var command = typeof(IntegrationBaseCommand<>).MakeGenericType(model.Type);
                    var handler = typeof(IRequestHandler<,>).MakeGenericType(command, typeof(int));
                    var handlerImplement = typeof(IntegrationBaseCommandHandler<>).MakeGenericType(model.Type);
                    services.AddTransient(handler, handlerImplement);
                    var commandArray = typeof(IntegrationArrayBaseCommand<>).MakeGenericType(model.Type);
                    var handlerArray = typeof(IRequestHandler<,>).MakeGenericType(commandArray, typeof(int));
                    var handlerArrayImplement = typeof(IntegrationArrayBaseCommandHandler<>).MakeGenericType(model.Type);
                    services.AddTransient(handlerArray, handlerArrayImplement);

                    // add genaric queries
                    var queries = typeof(IIntegrationQueries<>).MakeGenericType(model.Type);
                    var queriesImplement = typeof(IntegrationBaseQueries<>).MakeGenericType(model.Type);
                    services.AddTransient(queries, queriesImplement);

                    // add genaric repository
                    var repository = typeof(IIntegrationRepository<>).MakeGenericType(model.Type);
                    var repositoryImplement = typeof(IntegrationBaseRepository<>).MakeGenericType(model.Type);
                    services.AddTransient(repository, repositoryImplement);
                }
            }
        }
    }
}
