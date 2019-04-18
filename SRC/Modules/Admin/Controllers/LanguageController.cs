using MDM.UI.Languages.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;
using Admin.Commands.LanguageCommand;
using MediatR;
using Admin.UI.Extenstions;
using Common;
using Common.Models;
using Common.ViewModels;
using System.Linq;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class LanguageController : BaseController
    {
        private readonly ILanguageQueries languageQueries = null;
        private readonly IMediator mediator = null;
        public LanguageController(ILanguageQueries languageQueries, IMediator mediator = null)
        {
            this.languageQueries = languageQueries;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("gets")]
        public async Task<APIResult> Gets()
        {
            var rs = await languageQueries.Gets();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("gets/caption")]
        public async Task<APIResult> GetCaptions()
        {
            var rs = await languageQueries.GetCaptions();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("get/caption")]
        public async Task<APIResult> GetCaption(int captionId)
        {
            var rs = await languageQueries.GetCaption(captionId);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("update/caption")]
        public async Task<APIResult> UpdateCaption([FromBody]UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            if(rs == 0)
            {
                CacheProvider.TryGetValue(CachingConstant.Language, out List<Language> languages);
                CacheProvider.TryGetValue(CachingConstant.CationLanguage, out List<CaptionViewModel> captions);
                captions.Remove(captions.First(c => c.Id == command.Caption.Id));
                captions.Add(await languageQueries.GetCaption(command.Caption.Id));
                AdminExtensions.CreateLanguageFiles(languages, captions);
            }
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
