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
    public class UpdateProcessCommand : BaseCommand<int>
    {
        public Process Model { set; get; }
        public UpdateProcessCommand(Process process)
        {
            Model = process;
        }
    }

    public class UpdateProcessCommandHandler : BaseCommandHandler<UpdateProcessCommand, int>
    {
        private readonly IProcessRepository processRepository = null;
        private readonly IProcessQueries processQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public UpdateProcessCommandHandler(IProcessRepository processRepository, IProcessQueries processQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.processRepository = processRepository;
            this.processQueries = processQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<int> HandleCommand(UpdateProcessCommand request, CancellationToken cancellationToken)
        {
            Process process = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Process.NotExisted");
            }
            else
            {
                process = await processQueries.GetByIdAsync(request.Model.Id);
                if (process == null)
                {
                    throw new BusinessException("Process.NotExisted");
                }
            }

            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null || process.PartnerId != company.Id)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.IsNew = false;
                        request.Model.PartnerId = company.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        if (await processRepository.UpdateAsync(request.Model) > 0)
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
