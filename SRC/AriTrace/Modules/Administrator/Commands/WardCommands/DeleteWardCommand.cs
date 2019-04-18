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
    public class DeleteWardCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteWardCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteWardCommandHandler : BaseCommandHandler<DeleteWardCommand, int>
    {
        private readonly IWardRepository wardRepository = null;
        private readonly IWardQueries wardQueries = null;
        public DeleteWardCommandHandler(IWardRepository wardRepository, IWardQueries wardQueries)
        {
            this.wardRepository = wardRepository;
            this.wardQueries = wardQueries;
        }
        public override async Task<int> HandleCommand(DeleteWardCommand request, CancellationToken cancellationToken)
        {
            Ward ward = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Ward.NotSelected");
            }
            else
            {
                ward = await wardQueries.GetByIdAsync(request.Model);
                if (ward == null)
                    throw new BusinessException("Ward.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        ward.IsDeleted = true;
                        ward.ModifiedDate = DateTime.Now;
                        ward.ModifiedBy = request.LoginSession.Id;

                        if (await wardRepository.UpdateAsync(ward) > 0)
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
