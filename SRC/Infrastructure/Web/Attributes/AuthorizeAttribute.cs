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
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
    {
        public List<string> CustomizeRoles { set; get; }
        public AuthorizeUserAttribute(string roles = "")
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

                var response = CommonHelper.HttpGet(GlobalConfiguration.AuthenURI, $"api/user/checklogin?accesstoken={Uri.EscapeDataString(accessToken)}").Result;

                if(response.IsSuccess)
                {
                    var rs = JsonConvert.DeserializeObject<APIResult>(response.Content);
                    rs.Data = JsonConvert.DeserializeObject<UserSession>(JsonConvert.SerializeObject(rs.Data));
                    if(rs.Result == (int)UserHttpCode.Success)
                    {
                        context.HttpContext.Items.Add("UserSession", rs.Data);

                        // Anthen only
                        if (CustomizeRoles.Count == 0)
                        {
                            return;
                        }
                        
                        foreach (var role in CustomizeRoles)
                        {
                            //User has this role
                            if(((UserSession)rs.Data).Roles.Any(r => r == role))
                            {
                                return;
                            }
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
