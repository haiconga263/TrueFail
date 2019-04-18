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
    public class DeleteCompanyTypeCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteCompanyTypeCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteCompanyTypeCommandHandler : BaseCommandHandler<DeleteCompanyTypeCommand, int>
    {
        private readonly ICompanyTypeRepository companyTypeRepository = null;
        private readonly ICompanyTypeQueries companyTypeQueries = null;
        public DeleteCompanyTypeCommandHandler(ICompanyTypeRepository companyTypeRepository, ICompanyTypeQueries companyTypeQueries)
        {
            this.companyTypeRepository = companyTypeRepository;
            this.companyTypeQueries = companyTypeQueries;
        }
        public override async Task<int> HandleCommand(DeleteCompanyTypeCommand request, CancellationToken cancellationToken)
        {
            CompanyType companyType = null;
            if (request.Model == 0)
            {
                throw new BusinessException("CompanyType.NotSelected");
            }
            else
            {
                companyType = await companyTypeQueries.GetByIdAsync(request.Model);
                if (companyType == null)
                    throw new BusinessException("CompanyType.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        companyType.IsDeleted = true;
                        companyType.ModifiedDate = DateTime.Now;
                        companyType.ModifiedBy = request.LoginSession.Id;

                        if (await companyTypeRepository.UpdateAsync(companyType) > 0)
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
