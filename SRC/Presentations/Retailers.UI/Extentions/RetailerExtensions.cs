using Autofac;
using Common;
using Common.Implementations;
using MDM.UI.Languages.Interfaces;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Retailers.UI.Extentions
{
    public static class RetailerExtensions
    {
        public static void BuildRetailer(this IApplicationBuilder application)
        {
            //Caching caption languages
            ILanguageQueries queries = GlobalConfiguration.Container.Resolve<ILanguageQueries>();
            var captions = queries.GetCaptions().Result;
            CacheProvider.ICacheProviderInstance.TrySetValue(CachingConstant.CationLanguage, captions);

            //Caching languages
            var languages = queries.Gets().Result;
            CacheProvider.ICacheProviderInstance.TrySetValue(CachingConstant.Language, languages);
        }
    }
}
