using MDM.UI.Captions.Interfaces;
using MDM.UI.Captions.Models;
using Common.Exceptions;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CaptionCommands
{
    public class DeleteCaptionCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteCaptionCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteCaptionCommandHandler : BaseCommandHandler<DeleteCaptionCommand, int>
    {
        private readonly ICaptionRepository captionRepository = null;
        private readonly ICaptionQueries captionQueries = null;
        public DeleteCaptionCommandHandler(ICaptionRepository captionRepository, ICaptionQueries captionQueries)
        {
            this.captionRepository = captionRepository;
            this.captionQueries = captionQueries;
        }
        public override async Task<int> HandleCommand(DeleteCaptionCommand request, CancellationToken cancellationToken)
        {
            Caption caption = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Caption.NotSelected");
            }
            else
            {
                caption = await captionQueries.GetByIdAsync(request.Model);
                if (caption == null)
                    throw new BusinessException("Caption.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        caption.IsDeleted = true;
                        caption.ModifiedDate = DateTime.Now;
                        caption.ModifiedBy = request.LoginSession.Id;

                        if (await captionRepository.UpdateAsync(caption) > 0)
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
