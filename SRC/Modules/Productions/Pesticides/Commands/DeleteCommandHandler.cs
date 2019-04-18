using Common.Exceptions;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Pesticides.Commands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IPesticideRepository pesticideRepository = null;
        private readonly IPesticideQueries pesticideQueries = null;

        public DeleteCommandHandler(IPesticideRepository pesticideRepository, IPesticideQueries pesticideQueries)
        {
            this.pesticideRepository = pesticideRepository;
            this.pesticideQueries = pesticideQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var pesticide = await pesticideQueries.GetById(request.Id);
            if (pesticide == null)
            {
                throw new BusinessException("Pesticide.NotExisted");
            }
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        pesticide.IsDeleted = true;
                        pesticide = UpdateBuild(pesticide, request.LoginSession);

                        if (await pesticideRepository.Update(pesticide) > 0) rs = 0;
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
