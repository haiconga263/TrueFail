using Common;
using Common.Exceptions;
using Common.Helpers;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Web.Controls;

namespace Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method| AttributeTargets.Class)]
    public class AuthorizeInUserServiceAttribute : Attribute, IAuthorizationFilter
    {
        public List<string> CustomizeRoles { set; get; }
        public AuthorizeInUserServiceAttribute(string roles = "")
        {
            CustomizeRoles = roles.Split(",").ToList();
            CustomizeRoles.RemoveAll(r => string.Empty.Equals(r));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Request.Headers.Keys.Any(k => k.ToLower() == Constant.AccessTokenHeaderKey.ToLower()))
            {
                var headerToken = context.HttpContext.Request.Headers.FirstOrDefault(h => h.Key.ToLower() == Constant.AccessTokenHeaderKey.ToLower());
                var accessToken = headerToken.Value;

                UserSession userSession = null;

                if (Common.Implementations.CacheProvider.ICacheProviderInstance.TryGetValue(CachingConstant.UserLogin, out Dictionary<string, UserSession> dicSession))
                {
                    if (dicSession.ContainsKey(accessToken))
                    {
                        userSession = dicSession[accessToken];
                    }
                }

                if(userSession != null)
                {
                    context.HttpContext.Items.Add("UserSession", userSession);

                    // Anthen only
                    if (CustomizeRoles.Count == 0)
                    {
                        return;
                    }

                    foreach (var role in CustomizeRoles)
                    {
                        //User has this role
                        if ((userSession).Roles.Any(r => r == role))
                        {
                            return;
                        }
                    }
                }
            }

            // Anthen or not Anthen are ok
            if (CustomizeRoles.Count == 1 && CustomizeRoles.First().Equals("Another"))
            {
                return;
            }

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var result = new APIResult()
            {
                Result = -1,
                Data = null,
                ErrorMessage = new NotPermissionException().Message
            };
            context.Result = new ObjectResult(result);
        }
    }
}
