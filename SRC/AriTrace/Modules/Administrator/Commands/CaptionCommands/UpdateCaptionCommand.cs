using MDM.UI.CaptionLanguages.Interfaces;
using MDM.UI.Captions.Interfaces;
using MDM.UI.Captions.Models;
using MDM.UI.Captions.ViewModels;
using Common.Exceptions;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CaptionCommands
{
    public class UpdateCaptionCommand : BaseCommand<int>
    {
        public CaptionMultipleLanguage Model { set; get; }
        public UpdateCaptionCommand(CaptionMultipleLanguage caption)
        {
            Model = caption;
        }
    }

    public class UpdateCaptionCommandHandler : BaseCommandHandler<UpdateCaptionCommand, int>
    {
        private readonly ICaptionRepository captionRepository = null;
        private readonly ICaptionQueries captionQueries = null;

        private readonly ICaptionLanguageRepository captionLanguageRepository = null;
        private readonly ICaptionLanguageQueries captionLanguageQueries = null;
        public UpdateCaptionCommandHandler(ICaptionRepository captionRepository, ICaptionQueries captionQueries,
                                            ICaptionLanguageRepository captionLanguageRepository, ICaptionLanguageQueries captionLanguageQueries)
        {
            this.captionRepository = captionRepository;
            this.captionQueries = captionQueries;

            this.captionLanguageRepository = captionLanguageRepository;
            this.captionLanguageQueries = captionLanguageQueries;
        }

        public override async Task<int> HandleCommand(UpdateCaptionCommand request, CancellationToken cancellationToken)
        {
            CaptionMultipleLanguage caption = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Caption.NotExisted");
            }
            else
            {
                caption = await captionQueries.GetByIdAsync(request.Model.Id);
                if (caption == null)
                {
                    throw new BusinessException("Caption.NotExisted");
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
                        if (request.Model.Languages != null && request.Model.Languages.Count > 0)
                        {
                            foreach (var clang in request.Model.Languages)
                            {
                                if (!string.IsNullOrWhiteSpace(clang.Caption))
                                {
                                    if (clang.Id <= 0)
                                        await captionLanguageRepository.AddAsync(clang);
                                    else await captionLanguageRepository.UpdateAsync(clang);
                                }
                                else if (clang.Id > 0)
                                    await captionLanguageRepository.DeleteAsync(clang.Id);
                            }
                        }

                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        if (await captionRepository.UpdateAsync(request.Model) > 0)
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
