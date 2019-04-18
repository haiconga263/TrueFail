using MDM.UI.Languages.Interfaces;
using MDM.UI.Languages.Models;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
namespace Administrator.Commands.LanguageCommands
{
    public class InsertLanguageCommand : BaseCommand<int>
    {
        public Language Model { set; get; }
        public InsertLanguageCommand(Language language)
        {
            Model = language;
        }
    }

    public class InsertLanguageCommandHandler : BaseCommandHandler<InsertLanguageCommand, int>
    {
        private readonly ILanguageRepository languageRepository = null;
        private readonly ILanguageQueries languageQueries = null;
        public InsertLanguageCommandHandler(ILanguageRepository languageRepository, ILanguageQueries languageQueries)
        {
            this.languageRepository = languageRepository;
            this.languageQueries = languageQueries;
        }
        public override async Task<int> HandleCommand(InsertLanguageCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        id = await languageRepository.AddAsync(request.Model);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (id > 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return id;
        }
    }
}
