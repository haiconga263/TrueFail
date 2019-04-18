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
    public class InsertProvinceCommand : BaseCommand<int>
    {
        public Province Model { set; get; }
        public InsertProvinceCommand(Province province)
        {
            Model = province;
        }
    }

    public class InsertProvinceCommandHandler : BaseCommandHandler<InsertProvinceCommand, int>
    {
        private readonly IProvinceRepository provinceRepository = null;
        private readonly IProvinceQueries provinceQueries = null;
        public InsertProvinceCommandHandler(IProvinceRepository provinceRepository, IProvinceQueries provinceQueries)
        {
            this.provinceRepository = provinceRepository;
            this.provinceQueries = provinceQueries;
        }
        public override async Task<int> HandleCommand(InsertProvinceCommand request, CancellationToken cancellationToken)
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

                        id = await provinceRepository.AddAsync(request.Model);
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
