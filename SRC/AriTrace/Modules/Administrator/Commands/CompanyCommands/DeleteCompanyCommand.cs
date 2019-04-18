using Common.Exceptions;
using DAL;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Companies.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CompanyCommands
{
    public class DeleteCompanyCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteCompanyCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteCompanyCommandHandler : BaseCommandHandler<DeleteCompanyCommand, int>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;
        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;
        }
        public override async Task<int> HandleCommand(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Company.NotSelected");
            }
            else
            {
                company = await companyQueries.GetByIdAsync(request.Model);
                if (company == null)
                    throw new BusinessException("Company.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        company.IsDeleted = true;
                        company.ModifiedDate = DateTime.Now;
                        company.ModifiedBy = request.LoginSession.Id;

                        if (await companyRepository.UpdateAsync(company) > 0)
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
