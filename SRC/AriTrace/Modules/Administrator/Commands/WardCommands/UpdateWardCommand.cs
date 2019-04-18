using Common.Exceptions;
using DAL;
using MDM.UI.Wards.Interfaces;
using MDM.UI.Wards.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.WardCommands
{
    public class UpdateWardCommand : BaseCommand<int>
    {
        public Ward Model { set; get; }
        public UpdateWardCommand(Ward ward)
        {
            Model = ward;
        }
    }

    public class UpdateWardCommandHandler : BaseCommandHandler<UpdateWardCommand, int>
    {
        private readonly IWardRepository wardRepository = null;
        private readonly IWardQueries wardQueries = null;
        public UpdateWardCommandHandler(IWardRepository wardRepository, IWardQueries wardQueries)
        {
            this.wardRepository = wardRepository;
            this.wardQueries = wardQueries;
        }
        public override async Task<int> HandleCommand(UpdateWardCommand request, CancellationToken cancellationToken)
        {
            Ward ward = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Ward.NotExisted");
            }
            else
            {
                ward = await wardQueries.GetByIdAsync(request.Model.Id);
                if (ward == null)
                {
                    throw new BusinessException("Ward.NotExisted");
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
                        if (await wardRepository.UpdateAsync(request.Model) > 0)
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
