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
    public class DeleteRegionCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteRegionCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteRegionCommandHandler : BaseCommandHandler<DeleteRegionCommand, int>
    {
        private readonly IRegionRepository regionRepository = null;
        private readonly IRegionQueries regionQueries = null;
        public DeleteRegionCommandHandler(IRegionRepository regionRepository, IRegionQueries regionQueries)
        {
            this.regionRepository = regionRepository;
            this.regionQueries = regionQueries;
        }
        public override async Task<int> HandleCommand(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Region.NotSelected");
            }
            else
            {
                region = await regionQueries.GetByIdAsync(request.Model);
                if (region == null)
                    throw new BusinessException("Region.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        region.IsDeleted = true;
                        region.ModifiedDate = DateTime.Now;
                        region.ModifiedBy = request.LoginSession.Id;

                        if (await regionRepository.UpdateAsync(region) > 0)
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
