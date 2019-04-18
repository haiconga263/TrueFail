using Common.Exceptions;
using DAL;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Productions.Interfaces;
using GS1.UI.Productions.Models;
using MDM.UI.Companies.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Productions.Commands
{
    public class DeleteProductionCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteProductionCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteProductionCommandHandler : BaseCommandHandler<DeleteProductionCommand, int>
    {
        private readonly IProductionRepository productionRepository = null;
        private readonly IProductionQueries productionQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public DeleteProductionCommandHandler(IProductionRepository productionRepository, IProductionQueries productionQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.productionRepository = productionRepository;
            this.productionQueries = productionQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<int> HandleCommand(DeleteProductionCommand request, CancellationToken cancellationToken)
        {
            Production production = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Production.NotSelected");
            }
            else
            {
                production = await productionQueries.GetByIdAsync(request.Model);
                if (production == null)
                    throw new BusinessException("Production.NotSelected");
            }

            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null || production.PartnerId != company.Id)
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
                        production.IsDeleted = true;
                        production.ModifiedDate = DateTime.Now;
                        production.ModifiedBy = request.LoginSession.Id;

                        if (await productionRepository.UpdateAsync(production) > 0)
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
