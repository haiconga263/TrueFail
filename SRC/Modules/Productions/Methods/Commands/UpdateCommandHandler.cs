using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Farmers.Models;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Methods.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Productions.Methods.Commands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IMethodRepository methodRepository = null;
        private readonly IMethodQueries methodQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateCommandHandler(IMethodRepository methodRepository,
            IMethodQueries methodQueries, IStorageQueries storageQueries)
        {
            this.methodRepository = methodRepository;
            this.methodQueries = methodQueries;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Method == null || request.Method.Id == 0)
            {
                throw new BusinessException("Method.NotExisted");
            }

            var method = await methodQueries.GetById(request.Method.Id);
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
                        request.Method.CreatedDate = method.CreatedDate;
                        request.Method.CreatedBy = method.CreatedBy;
                        request.Method = UpdateBuild(request.Method, request.LoginSession);
                        request.Method.Code = string.IsNullOrWhiteSpace(method.Code)
                            ? (await storageQueries.GenarateCodeAsync(StorageKeys.MethodCode))
                            : method.Code;

                        rs = await methodRepository.Update(request.Method);

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
