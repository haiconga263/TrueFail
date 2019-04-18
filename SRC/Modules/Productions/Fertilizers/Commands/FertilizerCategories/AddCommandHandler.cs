using Common.Exceptions;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Fertilizers.Commands.FertilizerCategories
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IFertilizerCategoryRepository fertilizerCategoryRepository = null;
        private readonly IFertilizerCategoryQueries fertilizerCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(IFertilizerCategoryRepository fertilizerCategoryRepository, IFertilizerCategoryQueries fertilizerCategoryQueries, IStorageQueries storageQueries)
        {
            this.fertilizerCategoryRepository = fertilizerCategoryRepository;
            this.fertilizerCategoryQueries = fertilizerCategoryQueries;
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
                        fertilizerCategoryRepository.JoinTransaction(conn, trans);
                        fertilizerCategoryQueries.JoinTransaction(conn, trans);

                        if (request.FertilizerCategory == null)
                        {
                            throw new BusinessException("AddWrongInformation");
                        }
                        
                        request.FertilizerCategory.Code = await storageQueries.GenarateCodeAsync(StorageKeys.FertilizerCategoryCode);
                        request.FertilizerCategory = CreateBuild(request.FertilizerCategory, request.LoginSession);
                        var fertilizerCategoryId = await fertilizerCategoryRepository.Add(request.FertilizerCategory);

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
