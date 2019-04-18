using Common;
using Common.Interfaces;
using Common.Models;
using Common.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    public abstract class BaseController : Controller
    {
        public ICacheProvider CacheProvider => Common.Implementations.CacheProvider.ICacheProviderInstance;
        [JsonIgnore]
        public UserSession LoginSession { set; get; }
        public string LanguageCode { set; get; } = "vi"; //default
        private int languageId = 0;
        public int LanguageId
        {
            get
            {
                if(languageId != 0)
                {
                    return languageId;
                }
                if (CacheProvider.TryGetValue(CachingConstant.Language, out List<Language> languages))
                {
                    var language = languages.FirstOrDefault(l => l.Code == LanguageCode);
                    if(language != null)
                    {
                        return language.Id;
                    }

                }
                return 1; //default VN
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetCaption(string key)
        {
            if(CacheProvider.TryGetValue(CachingConstant.CationLanguage, out List<CaptionViewModel> captions))
            {
                var caption = captions.FirstOrDefault(c => c.Name == key);
                if(caption != null)
                {
                    var captionLanguage = caption.Languages.FirstOrDefault(cl => cl.LanguageId == LanguageId);
                    if(captionLanguage != null)
                    {
                        return captionLanguage.Caption;
                    }
                    else
                    {
                        return caption.DefaultCaption;
                    }
                }
            }
            return $"#{key}";
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public UserSession GetUserSession(string accessToken)
        {
            if(CacheProvider.TryGetValue(CachingConstant.UserLogin, out Dictionary<string, UserSession> dicSession))
            {
                if(dicSession.ContainsKey(accessToken))
                {
                    return dicSession[accessToken];
                }
            }
            return null;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void SetUserSession(UserSession userSession)
        {
            if(!CacheProvider.TryGetValue(CachingConstant.UserLogin, out Dictionary<string, UserSession> dicSession))
            {
                dicSession = new Dictionary<string, UserSession>
                {
                    { userSession.AccessToken, userSession }
                };
                CacheProvider.TrySetValue(CachingConstant.UserLogin, dicSession);
            }

            if (dicSession.ContainsKey(userSession.AccessToken))
            {
                dicSession[userSession.AccessToken] = userSession;
            }
            else
            {
                dicSession.Add(userSession.AccessToken, userSession);
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void RemoveUserSession(UserSession userSession)
        {
            if (CacheProvider.TryGetValue(CachingConstant.UserLogin, out Dictionary<string, UserSession> dicSession))
            {
                if (dicSession.ContainsKey(userSession.AccessToken))
                {
                    dicSession.Remove(userSession.AccessToken);
                }
            }
        }
    }
}
