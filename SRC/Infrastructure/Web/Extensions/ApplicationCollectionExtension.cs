using Common;
using Common.Implementations;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Web.Extensions
{
    public static class ApplicationCollectionExtension
    {
        public static void Build(this IApplicationBuilder application, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseSession();

            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                    System.IO.Directory.GetCurrentDirectory())
            });
            application.UseStaticFiles();

            application.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                routes.MapSpaFallbackRoute(
                     name: "spa-fallback",
                     defaults: new { controller = "Home", action = "Index" });
            });
        }

        public static void BuildDatabaseSchima(this IApplicationBuilder application)
        {

            //Get All parameter of sp from Sql and Save to Caching Memory
            SqlRepository sqlRepository = new SqlRepository();
            var sqlParams = sqlRepository.GetSqlParameters().Result;
            CacheProvider.ICacheProviderInstance.TrySetValue(CachingConstant.SpParameterKey, sqlParams);
            var definedtableInfor = sqlRepository.GetDefinedTableType().Result;
            CacheProvider.ICacheProviderInstance.TrySetValue(CachingConstant.DefinedTableType, definedtableInfor);
        }
    }
}
