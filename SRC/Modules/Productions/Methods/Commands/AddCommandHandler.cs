using Common.Exceptions;
using DAL;
using Productions.UI.Methods.Interfaces;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;
using MDM.UI.Farmers.Models;
using Common;

namespace Productions.Methods.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IMethodRepository methodRepository = null;
        private readonly IMethodQueries methodQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(IMethodRepository methodRepository,
            IMethodQueries methodQueries,
            IStorageQueries storageQueries)
        {
            this.methodRepository = methodRepository;
            this.methodQueries = methodQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Method == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        methodRepository.JoinTransaction(conn, trans);
                        methodQueries.JoinTransaction(conn, trans);

                        request.Method.Code = await storageQueries.GenarateCodeAsync(StorageKeys.MethodCode);
                        request.Method = CreateBuild(request.Method, request.LoginSession);
                        var methodId = await methodRepository.Add(request.Method);

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
