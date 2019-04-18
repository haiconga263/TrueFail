using Common.Exceptions;
using Common.Extensions;
using Common.Helpers;
using DAL;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.Materials.Commands
{
    public class InsertMaterialCommand : BaseCommand<int>
    {
        public Material Model { set; get; }
        public InsertMaterialCommand(Material material)
        {
            Model = material;
        }
    }

    public class InsertMaterialCommandHandler : BaseCommandHandler<InsertMaterialCommand, int>
    {
        private readonly IMaterialRepository _materialRepository = null;
        private readonly IMaterialQueries _materialQueries = null;

        private readonly ICompanyQueries _companyQueries = null;
        private readonly ISettingQueries _settingQueries = null;

        public InsertMaterialCommandHandler(IMaterialRepository materialRepository, IMaterialQueries materialQueries,
                                            ICompanyQueries _companyQueries,
                                            ISettingQueries settingQueries)
        {
            this._materialRepository = materialRepository;
            this._materialQueries = materialQueries;


            this._companyQueries = _companyQueries;
            this._settingQueries = settingQueries;

        }
        public override async Task<int> HandleCommand(InsertMaterialCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var id = 0;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.ParnerId = company.Id;
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        id = await _materialRepository.AddAsync((Material)request.Model);
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
