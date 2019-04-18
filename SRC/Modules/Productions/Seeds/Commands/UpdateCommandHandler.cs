using Admin.UI;
using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Farmers.Models;
using MDM.UI.Products.ViewModels;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Seeds.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Productions.Seeds.Commands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ISeedRepository seedRepository = null;
        private readonly ISeedQueries seedQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateCommandHandler(ISeedRepository seedRepository,
            ISeedQueries seedQueries, IStorageQueries storageQueries)
        {
            this.seedRepository = seedRepository;
            this.seedQueries = seedQueries;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Seed == null || request.Seed.Id == 0)
            {
                throw new BusinessException("Seed.NotExisted");
            }

            var seed = await seedQueries.GetById(request.Seed.Id);
            if (seed == null)
            {
                throw new BusinessException("Seed.NotExisted");
            }

            if (request.Seed.ProductId > 0)
            {
                var product = await WebHelper.HttpGet<ProductViewModel>(GlobalConfiguration.APIGateWayURI,
                    $"{AppUrl.GetProductById}?productId={request.Seed.ProductId}",
                    request.LoginSession.AccessToken, request.LoginSession.LanguageCode);

                if (product == null) { throw new BusinessException("Product.NotExisted"); }
            }
            else throw new BusinessException("Product.NotExisted");

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Seed.CreatedDate = seed.CreatedDate;
                        request.Seed.CreatedBy = seed.CreatedBy;
                        request.Seed = UpdateBuild(request.Seed, request.LoginSession);
                        request.Seed.Code = string.IsNullOrWhiteSpace(seed.Code)
                            ? (await storageQueries.GenarateCodeAsync(StorageKeys.SeedCode))
                            : seed.Code;

                        rs = await seedRepository.Update(request.Seed);

                        if (rs == 0)
                        {
                            return -1;
                        }

                        rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                        }
                    }
                }
            }

            return rs;
        }
    }
}
