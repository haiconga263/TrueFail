using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using HomePage.Web.Localization;
using Users.UI.Extensions;
using Web.Extensions;
using Web.Extentions;
using Web.Filters;
using Admin.UI.Extenstions;

namespace HomePage.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _hostingEnvironment;

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region snippet1
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            #endregion

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo(Languages.Vietnamese),
                        new CultureInfo(Languages.English),
                    };

                options.DefaultRequestCulture = new RequestCulture(Languages.Vietnamese);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

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


            var provider = services.Build(Configuration, _hostingEnvironment, true);

            return provider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                });
            app.UseCors("AllowSpecificOrigin");

            #region snippet2
            var supportedCultures = new[]
            {
                new CultureInfo(Languages.Vietnamese),
                new CultureInfo(Languages.English),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(Languages.Vietnamese),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            // To configure external authentication, 
            // see: http://go.microsoft.com/fwlink/?LinkID=532715
            //app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();
            #endregion

            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseExceptionHandler("/Home/Error");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "/{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "About",
                    template: "/about",
                    defaults: new { controller = "Home", action = "About" });

                routes.MapRoute(
                    name: "BuyWhere",
                    template: "/where-to-buy",
                    defaults: new { controller = "Home", action = "BuyWhere" });

                routes.MapRoute(
                     name: "BuyWhere.vi",
                     template: "/mua-o-dau",
                     defaults: new { controller = "Home", action = "BuyWhere" });

                routes.MapRoute(
                     name: "FarmerNetwork.vi",
                     template: "/mang-luoi-nong-dan",
                     defaults: new { controller = "Home", action = "FarmerNetwork" });

                routes.MapRoute(
                    name: "FarmerNetwork",
                    template: "/farmer-network",
                    defaults: new { controller = "Home", action = "FarmerNetwork" });

                routes.MapRoute(
                    name: "CookingRecipes",
                    template: "/recipes",
                    defaults: new { controller = "Home", action = "CookingRecipe" });

                routes.MapRoute(
                    name: "CookingRecipes.vi",
                    template: "/cong-thuc",
                    defaults: new { controller = "Home", action = "CookingRecipe" });

                routes.MapRoute(
                    name: "CookingRecipeDetail",
                    template: "/recipe-detail",
                    defaults: new { controller = "Home", action = "CookingRecipeDetail" });

                routes.MapRoute(
                    name: "CookingRecipeDetail.vi",
                    template: "/chi-tiet-cong-thuc",
                    defaults: new { controller = "Home", action = "CookingRecipeDetail" });

                routes.MapRoute(
                    name: "TheMedia.vi",
                    template: "/truyen-thong",
                    defaults: new { controller = "Home", action = "TheMedia" });

                routes.MapRoute(
                    name: "TheMedia",
                    template: "/the-media",
                    defaults: new { controller = "Home", action = "TheMedia" });

				routes.MapRoute(
				   name: "BlogGobalGreen",
				   template: "/blog-farming-green",
				   defaults: new { controller = "Home", action = "Blog", topicId = 2 });

				routes.MapRoute(
				   name: "BlogGobalGreen.vi",
				   template: "/canh-tac-xanh",
				   defaults: new { controller = "Home", action = "Blog", topicId = 2 });

				routes.MapRoute(
					name: "BlogGobalGreen_11",
					template: "/{name}-b{blogId}.html",
					defaults: new { controller = "Home", action = "GetBlog" });

				routes.MapRoute(
					name: "BlogGobalGreen_11.vi",
					template: "/{name}-b{blogId}.html",
					defaults: new { controller = "Home", action = "GetBlog" });

				routes.MapRoute(
                    name: "BlogGreenSpirit",
                    template: "/blog-spirit-green",
                    defaults: new { controller = "Home", action = "Blog", topicId = 1 });

                routes.MapRoute(
                   name: "BlogGreenSpirit.vi",
                   template: "/tinh-than-xanh",
                   defaults: new { controller = "Home", action = "Blog", topicId = 1 });

                routes.MapRoute(
                    name: "BlogTravelGreen",
                    template: "/blog-travel-green",
                    defaults: new { controller = "Home", action = "Blog", topicId = 3 });

                routes.MapRoute(
                   name: "BlogTravelGreen.vi",
                   template: "/tan-man",
                   defaults: new { controller = "Home", action = "Blog", topicId = 3 });

                routes.MapRoute(
                    name: "Gifts",
                    template: "/gifts",
                    defaults: new { controller = "Home", action = "Gifts" });

                routes.MapRoute(
                   name: "Gifts.vi",
                   template: "/qua-tang",
                   defaults: new { controller = "Home", action = "Gifts" });

                routes.MapRoute(
                    name: "WeWork",
                    template: "/we-work",
                    defaults: new { controller = "Home", action = "WeWork" });

                routes.MapRoute(
                   name: "WeWork.vi",
                   template: "/chung-toi-dang-lam-gi",
                   defaults: new { controller = "Home", action = "WeWork" });

                routes.MapRoute(
                    name: "FAQs",
                    template: "/faqs",
                    defaults: new { controller = "Home", action = "Faqs" });

                routes.MapRoute(
                    name: "FAQs.vi",
                    template: "/hoi-dap",
                    defaults: new { controller = "Home", action = "Faqs" });

                routes.MapRoute(
                    name: "About.vi",
                    template: "/ve-chung-toi",
                    defaults: new { controller = "Home", action = "About" });

                routes.MapRoute(
                    name: "Contact",
                    template: "/contact",
                    defaults: new { controller = "Home", action = "Contact" });

                routes.MapRoute(
                    name: "Contact.vi",
                    template: "/lien-he",
                    defaults: new { controller = "Home", action = "Contact" });

                routes.MapRoute(
                    name: "Post",
                    template: "/post",
                    defaults: new { controller = "Post", action = "Index" });

                routes.MapRoute(
                    name: "Post.vi",
                    template: "/bai-viet",
                    defaults: new { controller = "Post", action = "Index" });

                routes.MapRoute(
                    name: "PostDetail",
                    template: "/post/content/{id?}",
                    defaults: new { controller = "Post", action = "Detail" });

                routes.MapRoute(
                    name: "PostDetail.vi",
                    template: "/bai-viet/noi-dung/{id?}",
                    defaults: new { controller = "Post", action = "Detail" });

                routes.MapRoute(
                    name: "ProductDetail",
                    template: "/product/detail/{id?}",
                    defaults: new { controller = "Products", action = "Detail" });

                routes.MapRoute(
                    name: "ProductDetail.vi",
                    template: "/san-pham/chi-tiet/{id?}",
                    defaults: new { controller = "Products", action = "Detail" });


                routes.MapRoute(
                    name: "ProductDetail_1",
                    template: "/{name}-p{id}.html",
                    defaults: new { controller = "Products", action = "Detail" });

                routes.MapRoute(
                    name: "ProductDetail_1.vi",
                    template: "/{name}-p{id}.html",
                    defaults: new { controller = "Products", action = "Detail" });

                routes.MapRoute(
                    name: "Product_1",
                    template: "/{name}-c{cateId}",
                    defaults: new { controller = "Products", action = "Index" });

                routes.MapRoute(
                    name: "Product",
                    template: "/products",
                    defaults: new { controller = "Products", action = "Index" });

                routes.MapRoute(
                    name: "Product.vi",
                    template: "/danh-sach-san-pham",
                    defaults: new { controller = "Products", action = "Index" });

                routes.MapRoute(
                    name: "Product_1.vi",
                    template: "/{name}-c{cateId}",
                    defaults: new { controller = "Products", action = "Index" });

                routes.MapRoute(
                    name: "Cart",
                    template: "/cart",
                    defaults: new { controller = "Cart", action = "Index" });

                routes.MapRoute(
                    name: "Cart.vi",
                    template: "/gio-hang",
                    defaults: new { controller = "Cart", action = "Index" });

                routes.MapRoute(
                  name: "CheckOut",
                  template: "/checkout",
                  defaults: new { controller = "Cart", action = "CheckOut" });

                routes.MapRoute(
                    name: "CheckOut.vi",
                    template: "/thanh-toan",
                    defaults: new { controller = "Cart", action = "CheckOut" });

                routes.MapRoute(
                    name: "Errors",
                    template: "/error",
                    defaults: new { controller = "Errors", action = "CatchAll" });

                routes.MapRoute(
                    name: "GreenDaLatBlog",
                    template: "/blog-da-lat-xanh",
                    defaults: new { controller = "Home", action = "GetBlog", blogId = 1 });

                routes.MapRoute(
                    name: "GreenDaLatBlog.vi",
                    template: "/green-da-lat-blog",
                    defaults: new { controller = "Home", action = "GetBlog", blogId = 1 });

                routes.MapRoute(
                   name: "Trace",
                   template: "/trace",
                   defaults: new { controller = "Home", action = "Trace" });

                routes.MapRoute(
                    name: "Trace.vi",
                    template: "/truy-xuat",
                    defaults: new { controller = "Home", action = "Trace" });
            });

            app.LoadSession(env);
            app.BuildAdmin();
		}
	}
}