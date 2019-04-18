using Common.Exceptions;
using DAL;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Productions.Interfaces;
using GS1.UI.Productions.Models;
using GS1.UI.Productions.ViewModels;
using MDM.UI.Companies.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Productions.Commands
{
    public class UpdateProductionCommand : BaseCommand<int>
    {
        public ProductionInformation Model { set; get; }
        public UpdateProductionCommand(ProductionInformation production)
        {
            Model = production;
        }
    }

    public class UpdateProductionCommandHandler : BaseCommandHandler<UpdateProductionCommand, int>
    {
        private readonly IProductionRepository productionRepository = null;
        private readonly IProductionQueries productionQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public UpdateProductionCommandHandler(IProductionRepository productionRepository, IProductionQueries productionQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.productionRepository = productionRepository;
            this.productionQueries = productionQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<int> HandleCommand(UpdateProductionCommand request, CancellationToken cancellationToken)
        {
            Production production = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Production.NotExisted");
            }
            else
            {
                production = await productionQueries.GetByIdAsync(request.Model.Id);
                if (production == null)
                {
                    throw new BusinessException("Production.NotExisted");
                }
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
                        var gTIN = await gTINService.InsertOrUpdateGTINAsync(company.Id, request.Model.GTIN, request.LoginSession);
                        request.Model.GTINId = gTIN.Id;

                        request.Model.PartnerId = company.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        if (await productionRepository.UpdateAsync(request.Model) > 0)
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
