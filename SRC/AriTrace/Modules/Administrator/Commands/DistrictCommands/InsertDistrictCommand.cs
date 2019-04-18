using Common.Exceptions;
using DAL;
using MDM.UI.Districts.Interfaces;
using MDM.UI.Districts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
namespace Administrator.Commands.DistrictCommands
{
    public class InsertDistrictCommand : BaseCommand<int>
    {
        public District Model { set; get; }
        public InsertDistrictCommand(District district)
        {
            Model = district;
        }
    }

    public class InsertDistrictCommandHandler : BaseCommandHandler<InsertDistrictCommand, int>
    {
        private readonly IDistrictRepository districtRepository = null;
        private readonly IDistrictQueries districtQueries = null;
        public InsertDistrictCommandHandler(IDistrictRepository districtRepository, IDistrictQueries districtQueries)
        {
            this.districtRepository = districtRepository;
            this.districtQueries = districtQueries;
        }
        public override async Task<int> HandleCommand(InsertDistrictCommand request, CancellationToken cancellationToken)
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

                        id = await districtRepository.AddAsync(request.Model);
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
