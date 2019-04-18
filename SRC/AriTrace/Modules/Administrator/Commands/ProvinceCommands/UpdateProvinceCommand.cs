using Common.Exceptions;
using DAL;
using MDM.UI.Provinces.Interfaces;
using MDM.UI.Provinces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.ProvinceCommands
{
    public class UpdateProvinceCommand : BaseCommand<int>
    {
        public Province Model { set; get; }
        public UpdateProvinceCommand(Province province)
        {
            Model = province;
        }
    }

    public class UpdateProvinceCommandHandler : BaseCommandHandler<UpdateProvinceCommand, int>
    {
        private readonly IProvinceRepository provinceRepository = null;
        private readonly IProvinceQueries provinceQueries = null;
        public UpdateProvinceCommandHandler(IProvinceRepository provinceRepository, IProvinceQueries provinceQueries)
        {
            this.provinceRepository = provinceRepository;
            this.provinceQueries = provinceQueries;
        }
        public override async Task<int> HandleCommand(UpdateProvinceCommand request, CancellationToken cancellationToken)
        {
            Province province = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Province.NotExisted");
            }
            else
            {
                province = await provinceQueries.GetByIdAsync(request.Model.Id);
                if (province == null)
                {
                    throw new BusinessException("Province.NotExisted");
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
                        if (await provinceRepository.UpdateAsync(request.Model) > 0)
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
