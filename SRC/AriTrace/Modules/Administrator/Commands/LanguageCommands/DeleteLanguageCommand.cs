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
    public class DeleteLanguageCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteLanguageCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteLanguageCommandHandler : BaseCommandHandler<DeleteLanguageCommand, int>
    {
        private readonly ILanguageRepository languageRepository = null;
        private readonly ILanguageQueries languageQueries = null;
        public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, ILanguageQueries languageQueries)
        {
            this.languageRepository = languageRepository;
            this.languageQueries = languageQueries;
        }
        public override async Task<int> HandleCommand(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            Language language = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Language.NotSelected");
            }
            else
            {
                language = await languageQueries.GetByIdAsync(request.Model);
                if (language == null)
                    throw new BusinessException("Language.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        language.IsDeleted = true;
                        language.ModifiedDate = DateTime.Now;
                        language.ModifiedBy = request.LoginSession.Id;

                        if (await languageRepository.UpdateAsync(language) > 0)
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
