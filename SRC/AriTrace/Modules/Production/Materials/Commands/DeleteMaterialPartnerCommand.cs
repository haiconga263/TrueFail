using Common.Exceptions;
using DAL;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Models;
using MDM.UI.Companies.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.Materials.Commands
{
    public class DeleteMaterialCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteMaterialCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteMaterialCommandHandler : BaseCommandHandler<DeleteMaterialCommand, int>
    {
        private readonly IAccountRepository _materialRepository = null;
        private readonly IAccountQueries _materialQueries = null;

        private readonly ICompanyQueries _companyQueries = null;
        public DeleteMaterialCommandHandler(IAccountRepository materialRepository, IAccountQueries materialQueries, ICompanyQueries companyQueries)
        {
            this._materialRepository = materialRepository;
            this._materialQueries = materialQueries;

            this._companyQueries = companyQueries;
        }
        public override async Task<int> HandleCommand(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {

            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            Account material = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Account.NotExisted");
            }
            else
            {
                material = await _materialQueries.GetByIdAsync(request.Model);
                if (material == null)
                {
                    throw new BusinessException("Account.NotExisted");
                }
                else if (material.PartnerId != company.Id)
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
                        material.IsDeleted = true;
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
