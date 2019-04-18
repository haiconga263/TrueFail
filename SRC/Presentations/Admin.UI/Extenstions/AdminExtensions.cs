using Autofac;
using Common;
using Common.Implementations;
using Common.Models;
using Common.ViewModels;
using MDM.UI.Languages.Interfaces;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Admin.UI.Extenstions
{
    public static class AdminExtensions
    {
        public static void BuildAdmin(this IApplicationBuilder application)
        {
            //Caching caption languages
            ILanguageQueries queries = GlobalConfiguration.Container.Resolve<ILanguageQueries>();
            var captions = queries.GetCaptions().Result;
            CacheProvider.ICacheProviderInstance.TrySetValue(CachingConstant.CationLanguage, captions.ToList());

            //Caching languages
            var languages = queries.Gets().Result;
            CacheProvider.ICacheProviderInstance.TrySetValue(CachingConstant.Language, languages.ToList());

            //Write caption to files
            CreateLanguageFiles(languages, captions);
        }
        public static void CreateLanguageFiles(IEnumerable<Language> languages, IEnumerable<CaptionViewModel> captions)
        {
            foreach (var language in languages)
            {
                if (!Directory.Exists(Constant.AssetsLanguageFolder)) Directory.CreateDirectory(Constant.AssetsLanguageFolder);
                Dictionary<string, string> content = new Dictionary<string, string>();
                foreach (var caption in captions)
                {
                    var lang = caption.Languages.FirstOrDefault(l => l.LanguageId == language.Id);
                    content[caption.Name] = lang == null ? caption.DefaultCaption : lang.Caption;
                }
                string contentStr = JsonConvert.SerializeObject(content);
                File.WriteAllText($"{Constant.AssetsLanguageFolder}/{language.Code}.json", contentStr);
            }
        }
    }
}
