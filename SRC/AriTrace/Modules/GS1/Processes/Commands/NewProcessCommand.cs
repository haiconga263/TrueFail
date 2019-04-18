using Common.Exceptions;
using DAL;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Processes.Interfaces;
using GS1.UI.Processes.Models;
using GS1.UI.Processes.ViewModels;
using MDM.UI.Companies.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Processes.Commands
{
    public class NewProcessCommand : BaseCommand<ProcessInformation>
    {
        public NewProcessCommand() { }
    }

    public class NewProcessCommandHandler : BaseCommandHandler<NewProcessCommand, ProcessInformation>
    {
        private readonly IProcessService processService = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        public NewProcessCommandHandler(IProcessService processService,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this.processService = processService;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

        }
        public override async Task<ProcessInformation> HandleCommand(NewProcessCommand request, CancellationToken cancellationToken)
        {

            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                bool isThrownEx = false;
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        return await processService.NewProcessAsync(company.Id, request.LoginSession);
                    }
                    catch (Exception ex)
                    {
                        isThrownEx = true;
                        throw ex;
                    }
                    finally
                    {
                        if (!isThrownEx) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }
        }
    }
}
