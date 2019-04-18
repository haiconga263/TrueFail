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
    public class InsertCompanyTypeCommand : BaseCommand<int>
    {
        public CompanyType Model { set; get; }
        public InsertCompanyTypeCommand(CompanyType companyType)
        {
            Model = companyType;
        }
    }

    public class InsertCompanyTypeCommandHandler : BaseCommandHandler<InsertCompanyTypeCommand, int>
    {
        private readonly ICompanyTypeRepository companyTypeRepository = null;
        private readonly ICompanyTypeQueries companyTypeQueries = null;
        public InsertCompanyTypeCommandHandler(ICompanyTypeRepository companyTypeRepository, ICompanyTypeQueries companyTypeQueries)
        {
            this.companyTypeRepository = companyTypeRepository;
            this.companyTypeQueries = companyTypeQueries;
        }
        public override async Task<int> HandleCommand(InsertCompanyTypeCommand request, CancellationToken cancellationToken)
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

                        id = await companyTypeRepository.AddAsync(request.Model);
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
