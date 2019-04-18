using Common.Exceptions;
using DAL;
using Productions.UI.Seeds.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Seeds.Commands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ISeedRepository seedRepository = null;
        private readonly ISeedQueries seedQueries = null;

        public DeleteCommandHandler(ISeedRepository seedRepository, ISeedQueries seedQueries)
        {
            this.seedRepository = seedRepository;
            this.seedQueries = seedQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var seed = await seedQueries.GetById(request.Id);
            if (seed == null)
            {
                throw new BusinessException("Seed.NotExisted");
            }
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        seed.IsDeleted = true;
                        seed = UpdateBuild(seed, request.LoginSession);

                        if (await seedRepository.Update(seed) > 0) rs = 0;
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
