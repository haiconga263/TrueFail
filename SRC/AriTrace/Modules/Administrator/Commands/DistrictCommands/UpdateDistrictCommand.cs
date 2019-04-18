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
    public class UpdateDistrictCommand : BaseCommand<int>
    {
        public District Model { set; get; }
        public UpdateDistrictCommand(District district)
        {
            Model = district;
        }
    }

    public class UpdateDistrictCommandHandler : BaseCommandHandler<UpdateDistrictCommand, int>
    {
        private readonly IDistrictRepository districtRepository = null;
        private readonly IDistrictQueries districtQueries = null;
        public UpdateDistrictCommandHandler(IDistrictRepository districtRepository, IDistrictQueries districtQueries)
        {
            this.districtRepository = districtRepository;
            this.districtQueries = districtQueries;
        }
        public override async Task<int> HandleCommand(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            District district = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("District.NotExisted");
            }
            else
            {
                district = await districtQueries.GetByIdAsync(request.Model.Id);
                if (district == null)
                {
                    throw new BusinessException("District.NotExisted");
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
                        if (await districtRepository.UpdateAsync(request.Model) > 0)
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
