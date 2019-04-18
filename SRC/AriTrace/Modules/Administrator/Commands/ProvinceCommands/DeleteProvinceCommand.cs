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
    public class DeleteProvinceCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteProvinceCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteProvinceCommandHandler : BaseCommandHandler<DeleteProvinceCommand, int>
    {
        private readonly IProvinceRepository provinceRepository = null;
        private readonly IProvinceQueries provinceQueries = null;
        public DeleteProvinceCommandHandler(IProvinceRepository provinceRepository, IProvinceQueries provinceQueries)
        {
            this.provinceRepository = provinceRepository;
            this.provinceQueries = provinceQueries;
        }
        public override async Task<int> HandleCommand(DeleteProvinceCommand request, CancellationToken cancellationToken)
        {
            Province province = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Province.NotSelected");
            }
            else
            {
                province = await provinceQueries.GetByIdAsync(request.Model);
                if (province == null)
                    throw new BusinessException("Province.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        province.IsDeleted = true;
                        province.ModifiedDate = DateTime.Now;
                        province.ModifiedBy = request.LoginSession.Id;

                        if (await provinceRepository.UpdateAsync(province) > 0)
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
