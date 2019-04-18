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
    public class UpdateGrowingMethodCommand : BaseCommand<int>
    {
        public GrowingMethod Model { set; get; }
        public UpdateGrowingMethodCommand(GrowingMethod growingMethod)
        {
            Model = growingMethod;
        }
    }

    public class UpdateGrowingMethodCommandHandler : BaseCommandHandler<UpdateGrowingMethodCommand, int>
    {
        private readonly IGrowingMethodRepository growingMethodRepository = null;
        private readonly IGrowingMethodQueries growingMethodQueries = null;
        public UpdateGrowingMethodCommandHandler(IGrowingMethodRepository growingMethodRepository, IGrowingMethodQueries growingMethodQueries)
        {
            this.growingMethodRepository = growingMethodRepository;
            this.growingMethodQueries = growingMethodQueries;
        }
        public override async Task<int> HandleCommand(UpdateGrowingMethodCommand request, CancellationToken cancellationToken)
        {
            GrowingMethod growingMethod = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("GrowingMethod.NotExisted");
            }
            else
            {
                growingMethod = await growingMethodQueries.GetByIdAsync(request.Model.Id);
                if (growingMethod == null)
                {
                    throw new BusinessException("GrowingMethod.NotExisted");
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
                        if (await growingMethodRepository.UpdateAsync(request.Model) > 0)
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
