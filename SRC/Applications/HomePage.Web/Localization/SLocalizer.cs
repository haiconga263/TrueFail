using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HomePage.Web.Localization
{
    public class SLocalizer : StringLocalizer<SharedResource>
    {
        private readonly IStringLocalizer _internalLocalizer;

        public SLocalizer(IStringLocalizerFactory factory, IHttpContextAccessor httpContextAccessor) : base(factory)
        {
            CurrentLanguage = httpContextAccessor.HttpContext.GetRouteValue("lang") as string;
            if (string.IsNullOrEmpty(CurrentLanguage) || CurrentLanguage != Languages.English)
            {
                CurrentLanguage = Languages.Vietnamese;
            }

            _internalLocalizer = WithCulture(new CultureInfo(CurrentLanguage));
        }

        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return _internalLocalizer[name, arguments];
            }
        }

        public override LocalizedString this[string name]
        {
            get
            {
                return _internalLocalizer[name];
            }
        }

        public string CurrentLanguage { get; set; }
    }
}
