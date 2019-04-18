using Common.Exceptions;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Fertilizers.Commands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IFertilizerRepository fertilizerRepository = null;
        private readonly IFertilizerQueries fertilizerQueries = null;

        public DeleteCommandHandler(IFertilizerRepository fertilizerRepository, IFertilizerQueries fertilizerQueries)
        {
            this.fertilizerRepository = fertilizerRepository;
            this.fertilizerQueries = fertilizerQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var fertilizer = await fertilizerQueries.GetById(request.Id);
            if (fertilizer == null)
            {
                throw new BusinessException("Fertilizer.NotExisted");
            }
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        fertilizer.IsDeleted = true;
                        fertilizer = UpdateBuild(fertilizer, request.LoginSession);

                        if (await fertilizerRepository.Update(fertilizer) > 0) rs = 0;
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
