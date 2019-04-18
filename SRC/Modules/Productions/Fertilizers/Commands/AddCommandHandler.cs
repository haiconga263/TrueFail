using Common.Exceptions;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Fertilizers.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IFertilizerRepository fertilizerRepository = null;
        private readonly IFertilizerQueries fertilizerQueries = null;
        private readonly IFertilizerCategoryQueries fertilizerCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(IFertilizerRepository fertilizerRepository,
            IFertilizerQueries fertilizerQueries,
            IFertilizerCategoryQueries fertilizerCategoryQueries,
            IStorageQueries storageQueries)
        {
            this.fertilizerRepository = fertilizerRepository;
            this.fertilizerQueries = fertilizerQueries;
            this.fertilizerCategoryQueries = fertilizerCategoryQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if ((request.Fertilizer?.CategoryId ?? 0) > 0)
            {
                var category = await fertilizerCategoryQueries.GetById(request.Fertilizer.CategoryId ?? 0);
                if (category==null)
                {
                    throw new BusinessException("Category.NotExisted");
                }
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        fertilizerRepository.JoinTransaction(conn, trans);
                        fertilizerQueries.JoinTransaction(conn, trans);

                        if (request.Fertilizer == null)
                        {
                            throw new BusinessException("AddWrongInformation");
                        }

                        request.Fertilizer.Code = await storageQueries.GenarateCodeAsync(StorageKeys.FertilizerCode);
                        request.Fertilizer = CreateBuild(request.Fertilizer, request.LoginSession);
                        var fertilizerId = await fertilizerRepository.Add(request.Fertilizer);

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
