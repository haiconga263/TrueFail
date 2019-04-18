using Admin.UI;
using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Products.ViewModels;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Seeds.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Productions.Seeds.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ISeedRepository seedRepository = null;
        private readonly ISeedQueries seedQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(ISeedRepository seedRepository,
            ISeedQueries seedQueries,
            IStorageQueries storageQueries)
        {
            this.seedRepository = seedRepository;
            this.seedQueries = seedQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Seed == null)
            {
                throw new BusinessException("AddWrongInformation");
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
                        seedRepository.JoinTransaction(conn, trans);
                        seedQueries.JoinTransaction(conn, trans);

                        request.Seed.Code = await storageQueries.GenarateCodeAsync(StorageKeys.SeedCode);
                        request.Seed = CreateBuild(request.Seed, request.LoginSession);
                        var seedId = await seedRepository.Add(request.Seed);

                        rs = 0;
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
