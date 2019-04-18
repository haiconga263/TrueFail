using Autofac;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Users.UI.Interfaces;

namespace Users.UI.Extensions
{
    public static class UserExtension
    {
        public static void LoadSession(this IApplicationBuilder application, IHostingEnvironment environment)
        {
            var userAccessTokenQueries = GlobalConfiguration.Container.Resolve<IAccessTokenQueries>();
            var accessTokens = userAccessTokenQueries.Gets().Result;

            Dictionary<string, UserSession> dicSession = new Dictionary<string, UserSession>();
            foreach (var item in accessTokens)
            {
                dicSession.Add(item.AccessToken, item);
            }
            Common.Implementations.CacheProvider.ICacheProviderInstance.TrySetValue(CachingConstant.UserLogin, dicSession);
        }
    }
}
