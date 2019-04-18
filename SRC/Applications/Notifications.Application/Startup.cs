using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Messages.Jobs;
using Notifications.Messages.Services;
using Notifications.UI.Messages.Interfaces;
using System;
using Web.Extensions;
using Web.Extentions;
using Web.Filters;

namespace Notifications.Application
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public IConfigurationRoot Configuration { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(hostingEnvironment.ContentRootPath)
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
                  .AddEnvironmentVariables();
            Configuration = builder.Build();
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "HTTP API",
                    Version = "v1",
                    Description = "The Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });
                options.OperationFilter<MyHeaderFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddOptions().AddQuartz()
                .AddTransient<ProcessMessageJob>()
                //.AddTransient<IFcmClient, MockFcmClient>()
                .AddTransient<IMessagingSchedulerService, MessagingSchedulerService>()
                .AddTransient<IMessagingService, MessagingService>()
                .AddMvc();

            var provider = services.Build(_configuration, _hostingEnvironment);

            //services.BuildIntergration(_hostingEnvironment);

            return provider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.Build(env);

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
               });

            app.UseCors("AllowSpecificOrigin");

            app.UseCors("Everything")
               .UseStaticFiles()
               .UseQuartz()
               .UseMvc();

            //app.LoadSession(env);
            //app.BuildAdmin();
        }
    }
}
