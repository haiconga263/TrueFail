using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Controllers
{
    public abstract class BaseWebViewController : BaseController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.LoginSession = LoginSession;
        }
    }
}
