using Common.Exceptions;
using DAL;
using Productions.UI.Cultivations.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Cultivations.Commands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ICultivationRepository cultivationRepository = null;
        private readonly ICultivationQueries cultivationQueries = null;

        public DeleteCommandHandler(ICultivationRepository cultivationRepository, ICultivationQueries cultivationQueries)
        {
            this.cultivationRepository = cultivationRepository;
            this.cultivationQueries = cultivationQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var cultivation = await cultivationQueries.GetById(request.Id);
            if (cultivation == null)
            {
                throw new BusinessException("Cultivation.NotExisted");
            }
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        cultivation.IsDeleted = true;
                        cultivation = UpdateBuild(cultivation, request.LoginSession);

                        if (await cultivationRepository.Update(cultivation) > 0) rs = 0;
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
