using Common.Exceptions;
using DAL;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Fertilizers.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Fertilizers.Commands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IFertilizerRepository fertilizerRepository = null;
        private readonly IFertilizerQueries fertilizerQueries = null;
        private readonly IFertilizerCategoryQueries fertilizerCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateCommandHandler(IFertilizerRepository fertilizerRepository,
            IFertilizerQueries fertilizerQueries,
            IFertilizerCategoryQueries fertilizerCategoryQueries,
            IStorageQueries storageQueries)
        {
            this.fertilizerRepository = fertilizerRepository;
            this.fertilizerQueries = fertilizerQueries;
            this.fertilizerCategoryQueries = fertilizerCategoryQueries;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Fertilizer == null || request.Fertilizer.Id == 0)
            {
                throw new BusinessException("Fertilizer.NotExisted");
            }

            var fertilizer = await fertilizerQueries.GetById(request.Fertilizer.Id);
            if (fertilizer == null)
            {
                throw new BusinessException("Fertilizer.NotExisted");
            }

            if ((request.Fertilizer?.CategoryId ?? 0) > 0)
            {
                var category = await fertilizerCategoryQueries.GetById(request.Fertilizer.CategoryId ?? 0);
                if (category == null)
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
                        request.Fertilizer.CreatedDate = fertilizer.CreatedDate;
                        request.Fertilizer.CreatedBy = fertilizer.CreatedBy;
                        request.Fertilizer = UpdateBuild(request.Fertilizer, request.LoginSession);
                        request.Fertilizer.Code = string.IsNullOrWhiteSpace(fertilizer.Code)
                            ? (await storageQueries.GenarateCodeAsync(StorageKeys.FertilizerCode))
                            : fertilizer.Code;

                        rs = await fertilizerRepository.Update(request.Fertilizer);

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
