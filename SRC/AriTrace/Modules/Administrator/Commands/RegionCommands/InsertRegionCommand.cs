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
    public class InsertRegionCommand : BaseCommand<int>
    {
        public Region Model { set; get; }
        public InsertRegionCommand(Region region)
        {
            Model = region;
        }
    }

    public class InsertRegionCommandHandler : BaseCommandHandler<InsertRegionCommand, int>
    {
        private readonly IRegionRepository regionRepository = null;
        private readonly IRegionQueries regionQueries = null;
        public InsertRegionCommandHandler(IRegionRepository regionRepository, IRegionQueries regionQueries)
        {
            this.regionRepository = regionRepository;
            this.regionQueries = regionQueries;
        }
        public override async Task<int> HandleCommand(InsertRegionCommand request, CancellationToken cancellationToken)
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

                        id = await regionRepository.AddAsync(request.Model);
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
