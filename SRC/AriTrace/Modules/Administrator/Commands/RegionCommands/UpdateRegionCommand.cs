using Common.Exceptions;
using DAL;
using MDM.UI.Regions.Interfaces;
using MDM.UI.Regions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.RegionCommands
{
    public class UpdateRegionCommand : BaseCommand<int>
    {
        public Region Model { set; get; }
        public UpdateRegionCommand(Region region)
        {
            Model = region;
        }
    }

    public class UpdateRegionCommandHandler : BaseCommandHandler<UpdateRegionCommand, int>
    {
        private readonly IRegionRepository regionRepository = null;
        private readonly IRegionQueries regionQueries = null;
        public UpdateRegionCommandHandler(IRegionRepository regionRepository, IRegionQueries regionQueries)
        {
            this.regionRepository = regionRepository;
            this.regionQueries = regionQueries;
        }
        public override async Task<int> HandleCommand(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Region.NotExisted");
            }
            else
            {
                region = await regionQueries.GetByIdAsync(request.Model.Id);
                if (region == null)
                {
                    throw new BusinessException("Region.NotExisted");
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
                        if (await regionRepository.UpdateAsync(request.Model) > 0)
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
