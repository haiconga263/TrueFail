using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HomePage.Web.Localization
{
    public class LocalizationPipeline
    {
        public void Configure(IApplicationBuilder app)
        {

            var supportedCultures = new List<CultureInfo>
                                {
                                    new CultureInfo(Languages.Vietnamese),
                                    new CultureInfo(Languages.English),
                                };

            var options = new RequestLocalizationOptions()
            {

                DefaultRequestCulture = new RequestCulture(culture: Languages.Vietnamese, uiCulture: Languages.Vietnamese),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            options.RequestCultureProviders = new[] { new RouteDataRequestCultureProvider() { Options = options, RouteDataStringKey = "lang", UIRouteDataStringKey = "lang" } };

            app.UseRequestLocalization(options);
        }
    }
}
