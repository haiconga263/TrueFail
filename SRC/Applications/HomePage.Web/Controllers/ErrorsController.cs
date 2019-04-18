using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomePage.Web.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult CatchAll()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}