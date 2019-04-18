using Common;
using HomePage.Web.Localization;
using MDM.UI.Categories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;

namespace HomePage.Web.Infrastructure
{
    //[MiddlewareFilter(typeof(LocalizationPipeline))]
    public abstract class BaseController : Controller
    {
        protected readonly IStringLocalizer<SharedResource> _localizer;

        public BaseController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ViewData["ImagePath"] = GlobalConfiguration.ProductImagePath;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
	        
			return LocalRedirect(returnUrl);
        }

        public IActionResult Error()
        {
            return View();
        }

        private string _currentLanguage = string.Empty;
        protected string CurrentLanguage
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLanguage))
                {
                    return _currentLanguage;
                }

                if (string.IsNullOrEmpty(_currentLanguage))
                {
                    var feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                }

                return _currentLanguage;
            }
        }

        //public ActionResult RedirectToDefaultLanguage()
        //{
        //    var lang = CurrentLanguage;
        //        lang = "vi-VN";

        //    return RedirectToAction("Index", new { lang = lang });
        //}
    }
}
