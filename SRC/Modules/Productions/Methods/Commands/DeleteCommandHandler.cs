using Common.Exceptions;
using DAL;
using Productions.UI.Methods.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Methods.Commands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IMethodRepository methodRepository = null;
        private readonly IMethodQueries methodQueries = null;

        public DeleteCommandHandler(IMethodRepository methodRepository, IMethodQueries methodQueries)
        {
            this.methodRepository = methodRepository;
            this.methodQueries = methodQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var method = await methodQueries.GetById(request.Id);
            if (method == null)
            {
                throw new BusinessException("Method.NotExisted");
            }
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        method.IsDeleted = true;
                        method = UpdateBuild(method, request.LoginSession);

                        if (await methodRepository.Update(method) > 0) rs = 0;
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
