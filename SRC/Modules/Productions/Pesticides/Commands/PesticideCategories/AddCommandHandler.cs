using Common.Exceptions;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Pesticides.Commands.PesticideCategories
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IPesticideCategoryRepository pesticideCategoryRepository = null;
        private readonly IPesticideCategoryQueries pesticideCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(IPesticideCategoryRepository pesticideCategoryRepository, IPesticideCategoryQueries pesticideCategoryQueries, IStorageQueries storageQueries)
        {
            this.pesticideCategoryRepository = pesticideCategoryRepository;
            this.pesticideCategoryQueries = pesticideCategoryQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        pesticideCategoryRepository.JoinTransaction(conn, trans);
                        pesticideCategoryQueries.JoinTransaction(conn, trans);

                        if (request.PesticideCategory == null)
                        {
                            throw new BusinessException("AddWrongInformation");
                        }
                        
                        request.PesticideCategory.Code = await storageQueries.GenarateCodeAsync(StorageKeys.PesticideCategoryCode);
                        request.PesticideCategory = CreateBuild(request.PesticideCategory, request.LoginSession);
                        var pesticideCategoryId = await pesticideCategoryRepository.Add(request.PesticideCategory);

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
