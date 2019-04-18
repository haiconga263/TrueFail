using Common.Exceptions;
using DAL;
using Productions.UI.Plots.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Plots.Commands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IPlotRepository plotRepository = null;
        private readonly IPlotQueries plotQueries = null;

        public DeleteCommandHandler(IPlotRepository plotRepository, IPlotQueries plotQueries)
        {
            this.plotRepository = plotRepository;
            this.plotQueries = plotQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var plot = await plotQueries.GetById(request.Id);
            if (plot == null)
            {
                throw new BusinessException("Plot.NotExisted");
            }
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        plot.IsDeleted = true;
                        plot = UpdateBuild(plot, request.LoginSession);

                        if (await plotRepository.Update(plot) > 0) rs = 0;
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
