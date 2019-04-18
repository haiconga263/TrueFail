using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Languages.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.LanguageCommand
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ILanguageRepository languageRepository = null;
        private readonly ILanguageQueries languageQueries = null;
        public UpdateCommandHandler(ILanguageRepository languageRepository, ILanguageQueries languageQueries)
        {
            this.languageRepository = languageRepository;
            this.languageQueries = languageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Caption == null || request.Caption.Id == 0)
            {
                throw new BusinessException("Caption.NotExisted");
            }

            var caption = await languageQueries.GetCaption(request.Caption.Id);
            if(caption == null)
            {
                throw new BusinessException("Caption.NotExisted");
            }

            var rs = -1;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        languageRepository.JoinTransaction(conn, trans);

                        caption.Type = request.Caption.Type;
                        caption.DefaultCaption = request.Caption.DefaultCaption;
                        caption.Languages = request.Caption.Languages;
                        if(await languageRepository.UpdateCaption(caption) != 0)
                        {
                            return rs;
                        }

                        await languageRepository.DeleteCaptionLanguages(caption.Id);
                        foreach (var language in caption.Languages)
                        {
                            if(await languageRepository.AddCaptionLanguage(language) == 0)
                            {
                                return rs;
                            }
                        }

                        rs = 0;
                    }
                    finally
                    {
                        if(rs == 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                        }

                    }
                }
            }

            return rs;
        }
    }
}
