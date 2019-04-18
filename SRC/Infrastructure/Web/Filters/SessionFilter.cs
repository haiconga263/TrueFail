using Common;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;

namespace Web.Filters
{
    public class SessionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context){}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            UserSession userSession = null;
            if(context.HttpContext.Items.TryGetValue("UserSession", out object value))
            {
                userSession = (UserSession)value;
            }

            if (context.HttpContext.Request.Headers.Keys.Any(k => k.ToLower() == Constant.LanguageHeaderKey.ToLower()))
            {
                var headerLanguage = context.HttpContext.Request.Headers.FirstOrDefault(h => h.Key.ToLower() == Constant.LanguageHeaderKey.ToLower());
                var language = headerLanguage.Value.ToString();
                // Set Language to Controller
                context.Controller.GetType().GetProperty("LanguageCode")?.SetValue(context.Controller, language);
            }

            // Set LoginSession to Controller
            context.Controller.GetType().GetProperty("LoginSession")?.SetValue(context.Controller, userSession);

            // With add Login session to CommandBase
            foreach (var item in context.ActionArguments)
            {
                if(item.Value?.GetType() == null)
                {
                    continue;
                }
                var type = item.Value.GetType().BaseType;
                if(type != null)
                {
                    if (type.Name.Contains("BaseCommand"))
                    {
                        PropertyInfo[] infors = item.Value.GetType().GetProperties();
                        var loginSession = infors.ToList().Find(i => i.Name == "LoginSession"); //Get login session in parameter CommandBase of Request
                        if (loginSession != null)
                        {
                            loginSession.SetValue(context.ActionArguments[item.Key], userSession);
                        }
                    }
                }
            }

            // With add Login session to QueriesBase: Check with to GetRuntimeFields and GetFields
            foreach (var item in context.Controller.GetType().GetFields())
            {
                var interf = item.FieldType.GetInterfaces().ToList().Find(i => i == typeof(IBaseQueries));
                if (interf != null)
                {
                    var field = item.GetValue(context.Controller);
                    ((IBaseQueries)field).LoginSession = userSession;
                }
            }
            foreach (var item in context.Controller.GetType().GetRuntimeFields())
            {
                var interf = item.FieldType.GetInterfaces().ToList().Find(i => i == typeof(IBaseQueries));
                if (interf != null)
                {
                    var field = item.GetValue(context.Controller);
                    ((IBaseQueries)field).LoginSession = userSession;
                }
            }

        }
    }
}
