using Common.Exceptions;
using DAL;
using MDM.UI.GrowingMethods.Interfaces;
using MDM.UI.GrowingMethods.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.GrowingMethodCommands
{
    public class DeleteGrowingMethodCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteGrowingMethodCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteGrowingMethodCommandHandler : BaseCommandHandler<DeleteGrowingMethodCommand, int>
    {
        private readonly IGrowingMethodRepository growingMethodRepository = null;
        private readonly IGrowingMethodQueries growingMethodQueries = null;
        public DeleteGrowingMethodCommandHandler(IGrowingMethodRepository growingMethodRepository, IGrowingMethodQueries growingMethodQueries)
        {
            this.growingMethodRepository = growingMethodRepository;
            this.growingMethodQueries = growingMethodQueries;
        }
        public override async Task<int> HandleCommand(DeleteGrowingMethodCommand request, CancellationToken cancellationToken)
        {
            GrowingMethod growingMethod = null;
            if (request.Model == 0)
            {
                throw new BusinessException("GrowingMethod.NotSelected");
            }
            else
            {
                growingMethod = await growingMethodQueries.GetByIdAsync(request.Model);
                if (growingMethod == null)
                    throw new BusinessException("GrowingMethod.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        growingMethod.IsDeleted = true;
                        growingMethod.ModifiedDate = DateTime.Now;
                        growingMethod.ModifiedBy = request.LoginSession.Id;

                        if (await growingMethodRepository.UpdateAsync(growingMethod) > 0)
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
