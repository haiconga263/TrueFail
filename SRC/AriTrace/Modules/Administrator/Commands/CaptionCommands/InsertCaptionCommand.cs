using MDM.UI.CaptionLanguages.Interfaces;
using MDM.UI.Captions.Interfaces;
using MDM.UI.Captions.Models;
using MDM.UI.Captions.ViewModels;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
namespace Administrator.Commands.CaptionCommands
{
    public class InsertCaptionCommand : BaseCommand<int>
    {
        public CaptionMultipleLanguage Model { set; get; }
        public InsertCaptionCommand(CaptionMultipleLanguage caption)
        {
            Model = caption;
        }
    }

    public class InsertCaptionCommandHandler : BaseCommandHandler<InsertCaptionCommand, int>
    {
        private readonly ICaptionRepository captionRepository = null;
        private readonly ICaptionQueries captionQueries = null;

        private readonly ICaptionLanguageRepository captionLanguageRepository = null;
        private readonly ICaptionLanguageQueries captionLanguageQueries = null;
        public InsertCaptionCommandHandler(ICaptionRepository captionRepository, ICaptionQueries captionQueries,
                                            ICaptionLanguageRepository captionLanguageRepository, ICaptionLanguageQueries captionLanguageQueries)
        {
            this.captionRepository = captionRepository;
            this.captionQueries = captionQueries;

            this.captionLanguageRepository = captionLanguageRepository;
            this.captionLanguageQueries = captionLanguageQueries;
        }

        public override async Task<int> HandleCommand(InsertCaptionCommand request, CancellationToken cancellationToken)
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

                        id = await captionRepository.AddAsync(request.Model);

                        if (request.Model.Languages != null && request.Model.Languages.Count > 0)
                        {
                            foreach (var clang in request.Model.Languages)
                            {
                                if (clang.Id <= 0 && !string.IsNullOrWhiteSpace(clang.Caption))
                                {
                                    clang.CaptionId = id;
                                    await captionLanguageRepository.AddAsync(clang);
                                }
                            }
                        }
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
