using Common.Exceptions;
using DAL;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Fertilizers.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Fertilizers.Commands.FertilizerCategories
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IFertilizerCategoryRepository fertilizerCategoryRepository = null;
        private readonly IFertilizerCategoryQueries fertilizerCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateCommandHandler(IFertilizerCategoryRepository fertilizerCategoryRepository,
            IFertilizerCategoryQueries fertilizerCategoryQueries,
            IStorageQueries storageQueries)
        {
            this.fertilizerCategoryRepository = fertilizerCategoryRepository;
            this.fertilizerCategoryQueries = fertilizerCategoryQueries;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.FertilizerCategory == null || request.FertilizerCategory.Id == 0)
            {
                throw new BusinessException("FertilizerCategory.NotExisted");
            }

            var fertilizerCategory = await fertilizerCategoryQueries.GetById(request.FertilizerCategory.Id);
            if (fertilizerCategory == null)
            {
                throw new BusinessException("FertilizerCategory.NotExisted");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.FertilizerCategory.CreatedDate = fertilizerCategory.CreatedDate;
                        request.FertilizerCategory.CreatedBy = fertilizerCategory.CreatedBy;
                        request.FertilizerCategory = UpdateBuild(request.FertilizerCategory, request.LoginSession);
                        request.FertilizerCategory.Code = string.IsNullOrWhiteSpace(fertilizerCategory.Code)
                            ? (await storageQueries.GenarateCodeAsync(StorageKeys.FertilizerCategoryCode))
                            : fertilizerCategory.Code;
                        rs = await fertilizerCategoryRepository.Update(request.FertilizerCategory);

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
