using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Farmers.Models;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Plots.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Productions.Plots.Commands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IPlotRepository plotRepository = null;
        private readonly IPlotQueries plotQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateCommandHandler(IPlotRepository plotRepository,
            IPlotQueries plotQueries, IStorageQueries storageQueries)
        {
            this.plotRepository = plotRepository;
            this.plotQueries = plotQueries;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Plot == null || request.Plot.Id == 0)
            {
                throw new BusinessException("Plot.NotExisted");
            }

            var plot = await plotQueries.GetById(request.Plot.Id);
            if (plot == null)
            {
                throw new BusinessException("Plot.NotExisted");
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
                        request.Plot.CreatedDate = plot.CreatedDate;
                        request.Plot.CreatedBy = plot.CreatedBy;
                        request.Plot = UpdateBuild(request.Plot, request.LoginSession);
                        request.Plot.Code = string.IsNullOrWhiteSpace(plot.Code)
                            ? (await storageQueries.GenarateCodeAsync(StorageKeys.PlotCode))
                            : plot.Code;

                        rs = await plotRepository.Update(request.Plot);

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
