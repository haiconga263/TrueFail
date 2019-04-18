using Common.Exceptions;
using DAL;
using Productions.UI.CultivationActivities.Interfaces;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;
using MDM.UI.Farmers.Models;
using Common;
using Productions.UI.Plots;

namespace Productions.CultivationActivities.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ICultivationActivityRepository cultivationActivityRepository = null;
        private readonly ICultivationActivityQueries cultivationActivityQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(ICultivationActivityRepository cultivationActivityRepository,
            ICultivationActivityQueries cultivationActivityQueries,
            IStorageQueries storageQueries)
        {
            this.cultivationActivityRepository = cultivationActivityRepository;
            this.cultivationActivityQueries = cultivationActivityQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.CultivationActivity == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if ((request.CultivationActivity.FarmerId ?? 0) > 0)
            {
                var farmer = await WebHelper.HttpGet<Farmer>(GlobalConfiguration.APIGateWayURI,
                    $"{Farmers.UI.FarmerUrl.ApiGet}?farmerId={request.CultivationActivity.FarmerId}",
                    request.LoginSession.AccessToken, request.LoginSession.LanguageCode);

                if (farmer == null) { throw new BusinessException("Farmer.NotExisted"); }
            }
            else if (request.CultivationActivity.FarmerId != null)
            {
                throw new BusinessException("Farmer.NotExisted");
            }

            if (request.CultivationActivity.PlotId > 0)
            {
                var product = await WebHelper.HttpGet<Farmer>(GlobalConfiguration.APIGateWayURI,
                    $"{PlotUrl.ApiGet}?id={request.CultivationActivity.PlotId}",
                    request.LoginSession.AccessToken, request.LoginSession.LanguageCode);

                if (product == null) { throw new BusinessException("Plot.NotExisted"); }
            }
            else throw new BusinessException("Plot.NotExisted");

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        cultivationActivityRepository.JoinTransaction(conn, trans);
                        cultivationActivityQueries.JoinTransaction(conn, trans);

                        request.CultivationActivity = CreateBuild(request.CultivationActivity, request.LoginSession);
                        var cultivationActivityId = await cultivationActivityRepository.Add(request.CultivationActivity);

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
