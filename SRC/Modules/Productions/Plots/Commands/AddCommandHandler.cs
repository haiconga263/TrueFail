using Common.Exceptions;
using DAL;
using Productions.UI.Plots.Interfaces;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;
using MDM.UI.Farmers.Models;
using Common;

namespace Productions.Plots.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IPlotRepository plotRepository = null;
        private readonly IPlotQueries plotQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(IPlotRepository plotRepository,
            IPlotQueries plotQueries,
            IStorageQueries storageQueries)
        {
            this.plotRepository = plotRepository;
            this.plotQueries = plotQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Plot == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if (request.Plot.FarmerId > 0)
            {
                var farmer = await WebHelper.HttpGet<Farmer>(GlobalConfiguration.APIGateWayURI,
                    $"{Farmers.UI.FarmerUrl.ApiGet}?farmerId={request.Plot.FarmerId}",
                    request.LoginSession.AccessToken, request.LoginSession.LanguageCode);

                if (farmer == null) { throw new BusinessException("Farmer.NotExisted"); }
            }
            else { throw new BusinessException("Farmer.NotExisted"); }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        plotRepository.JoinTransaction(conn, trans);
                        plotQueries.JoinTransaction(conn, trans);

                        request.Plot.Code = await storageQueries.GenarateCodeAsync(StorageKeys.PlotCode);
                        request.Plot = CreateBuild(request.Plot, request.LoginSession);
                        var plotId = await plotRepository.Add(request.Plot);

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
