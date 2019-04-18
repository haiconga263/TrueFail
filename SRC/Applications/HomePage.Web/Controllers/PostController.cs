using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomePage.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HomePage.Web.Controllers
{
    public class PostController : BaseController
    {
	    public PostController(IStringLocalizer<SharedResource> localizer) : base(localizer)
	    {
	    }

	    public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}