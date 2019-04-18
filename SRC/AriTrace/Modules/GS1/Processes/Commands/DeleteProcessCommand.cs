using Common.Exceptions;
using DAL;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Processes.Interfaces;
using GS1.UI.Processes.Models;
using MDM.UI.Companies.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Processes.Commands
{
    public class DeleteProcessCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteProcessCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteProcessCommandHandler : BaseCommandHandler<DeleteProcessCommand, int>
    {
        private readonly IProcessRepository processRepository = null;
        private readonly IProcessQueries processQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public DeleteProcessCommandHandler(IProcessRepository processRepository, IProcessQueries processQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.processRepository = processRepository;
            this.processQueries = processQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<int> HandleCommand(DeleteProcessCommand request, CancellationToken cancellationToken)
        {
            Process process = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Process.NotSelected");
            }
            else
            {
                process = await processQueries.GetByIdAsync(request.Model);
                if (process == null)
                    throw new BusinessException("Process.NotSelected");
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
                        if (process.IsNew)
                        {
                            if (await processRepository.DeleteAsync(process.Id) > 0)
                                rs = 0;
                        }
                        else
                        {
                            process.IsDeleted = true;
                            process.ModifiedDate = DateTime.Now;
                            process.ModifiedBy = request.LoginSession.Id;
                            if (await processRepository.UpdateAsync(process) > 0)
                                rs = 0;
                        }

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
