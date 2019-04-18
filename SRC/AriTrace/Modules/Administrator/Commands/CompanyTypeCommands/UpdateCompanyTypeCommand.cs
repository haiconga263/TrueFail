using Common.Exceptions;
using DAL;
using MDM.UI.CompanyTypes.Interfaces;
using MDM.UI.CompanyTypes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CompanyTypeCommands
{
    public class UpdateCompanyTypeCommand : BaseCommand<int>
    {
        public CompanyType Model { set; get; }
        public UpdateCompanyTypeCommand(CompanyType companyType)
        {
            Model = companyType;
        }
    }

    public class UpdateCompanyTypeCommandHandler : BaseCommandHandler<UpdateCompanyTypeCommand, int>
    {
        private readonly ICompanyTypeRepository companyTypeRepository = null;
        private readonly ICompanyTypeQueries companyTypeQueries = null;
        public UpdateCompanyTypeCommandHandler(ICompanyTypeRepository companyTypeRepository, ICompanyTypeQueries companyTypeQueries)
        {
            this.companyTypeRepository = companyTypeRepository;
            this.companyTypeQueries = companyTypeQueries;
        }
        public override async Task<int> HandleCommand(UpdateCompanyTypeCommand request, CancellationToken cancellationToken)
        {
            CompanyType companyType = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("CompanyType.NotExisted");
            }
            else
            {
                companyType = await companyTypeQueries.GetByIdAsync(request.Model.Id);
                if (companyType == null)
                {
                    throw new BusinessException("CompanyType.NotExisted");
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
                        if (await companyTypeRepository.UpdateAsync(request.Model) > 0)
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
