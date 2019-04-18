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
    public class DeleteDistrictCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteDistrictCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteDistrictCommandHandler : BaseCommandHandler<DeleteDistrictCommand, int>
    {
        private readonly IDistrictRepository districtRepository = null;
        private readonly IDistrictQueries districtQueries = null;
        public DeleteDistrictCommandHandler(IDistrictRepository districtRepository, IDistrictQueries districtQueries)
        {
            this.districtRepository = districtRepository;
            this.districtQueries = districtQueries;
        }
        public override async Task<int> HandleCommand(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            District district = null;
            if (request.Model == 0)
            {
                throw new BusinessException("District.NotSelected");
            }
            else
            {
                district = await districtQueries.GetByIdAsync(request.Model);
                if (district == null)
                    throw new BusinessException("District.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        district.IsDeleted = true;
                        district.ModifiedDate = DateTime.Now;
                        district.ModifiedBy = request.LoginSession.Id;

                        if (await districtRepository.UpdateAsync(district) > 0)
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
