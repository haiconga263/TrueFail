using MDM.UI.Languages.Interfaces;
using MDM.UI.Languages.Models;
using Common.Exceptions;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.LanguageCommands
{
    public class UpdateLanguageCommand : BaseCommand<int>
    {
        public Language Model { set; get; }
        public UpdateLanguageCommand(Language language)
        {
            Model = language;
        }
    }

    public class UpdateLanguageCommandHandler : BaseCommandHandler<UpdateLanguageCommand, int>
    {
        private readonly ILanguageRepository languageRepository = null;
        private readonly ILanguageQueries languageQueries = null;
        public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, ILanguageQueries languageQueries)
        {
            this.languageRepository = languageRepository;
            this.languageQueries = languageQueries;
        }
        public override async Task<int> HandleCommand(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            Language language = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Language.NotExisted");
            }
            else
            {
                language = await languageQueries.GetByIdAsync(request.Model.Id);
                if (language == null)
                {
                    throw new BusinessException("Language.NotExisted");
                }
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        if (await languageRepository.UpdateAsync(request.Model) > 0)
                            rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }
            return rs;
        }
    }
}
