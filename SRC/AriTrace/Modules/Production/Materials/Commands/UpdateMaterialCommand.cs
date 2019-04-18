using Common.Exceptions;
using Common.Extensions;
using DAL;
using MDM.UI.Companies.Interfaces;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.Materials.Commands
{
    public class UpdateMaterialCommand : BaseCommand<int>
    {
        public Material Model { set; get; }
        public UpdateMaterialCommand(Material material)
        {
            Model = material;
        }
    }

    public class UpdateMaterialCommandHandler : BaseCommandHandler<UpdateMaterialCommand, int>
    {
        private readonly IMaterialRepository _materialRepository = null;
        private readonly IMaterialQueries _materialQueries = null;

        private readonly ICompanyQueries _companyQueries = null;

        public UpdateMaterialCommandHandler(IMaterialRepository materialRepository, IMaterialQueries materialQueries,
                                            ICompanyQueries companyQueries)
        {
            this._materialRepository = materialRepository;
            this._materialQueries = materialQueries;

            this._companyQueries = companyQueries;
        }
        public override async Task<int> HandleCommand(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            Material material = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Material.NotExisted");
            }
            else
            {
                material = await _materialQueries.GetByIdAsync(request.Model.Id);
                if (material == null)
                {
                    throw new BusinessException("Material.NotExisted");
                }
                else if (material.ParnerId != company.Id)
                {
                    throw new BusinessException("Common.NoPermission");
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
                        material.Id = request.Model.Id;
                        material.Code = request.Model.Code;
                        material.Name = request.Model.Name;
                        material.Description = request.Model.Description;
                        material.ProductId = request.Model.ProductId;
                        material.ParnerId = request.Model.ParnerId;
                        material.IsUsed = request.Model.IsUsed;
                        //material.IsDeleted = request.Model.IsDeleted;
                        //material.CreatedDate = request.Model.CreatedDate;
                        //material.CreatedBy = request.Model.CreatedBy;
                        material.ModifiedDate = DateTime.Now;
                        material.ModifiedBy = request.LoginSession.Id;

                        if (await _materialRepository.UpdateAsync(material) > 0)
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
