using Common.Exceptions;
using DAL;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Productions.Interfaces;
using GS1.UI.Productions.ViewModels;
using MDM.UI.Companies.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Productions.Commands
{
    public class InsertProductionCommand : BaseCommand<int>
    {
        public ProductionInformation Model { set; get; }
        public InsertProductionCommand(ProductionInformation production)
        {
            Model = production;
        }
    }

    public class InsertProductionCommandHandler : BaseCommandHandler<InsertProductionCommand, int>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IProductionRepository productionRepository = null;
        private readonly IProductionQueries productionQueries = null;

        private readonly IGTINService gTINService = null;

        public InsertProductionCommandHandler(IProductionRepository productionRepository, IProductionQueries productionQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.productionRepository = productionRepository;
            this.productionQueries = productionQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }

        public override async Task<int> HandleCommand(InsertProductionCommand request, CancellationToken cancellationToken)
        {
            var id = 0;

            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var gTIN = await gTINService.InsertOrUpdateGTINAsync(company.Id, request.Model.GTIN, request.LoginSession);
                        request.Model.GTINId = gTIN.Id;

                        request.Model.PartnerId = company.Id;
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        id = await productionRepository.AddAsync(request.Model);
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
