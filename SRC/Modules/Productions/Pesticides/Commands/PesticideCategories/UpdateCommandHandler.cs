using Common.Exceptions;
using DAL;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Pesticides.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Pesticides.Commands.PesticideCategories
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IPesticideCategoryRepository pesticideCategoryRepository = null;
        private readonly IPesticideCategoryQueries pesticideCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateCommandHandler(IPesticideCategoryRepository pesticideCategoryRepository,
            IPesticideCategoryQueries pesticideCategoryQueries,
            IStorageQueries storageQueries)
        {
            this.pesticideCategoryRepository = pesticideCategoryRepository;
            this.pesticideCategoryQueries = pesticideCategoryQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.PesticideCategory == null || request.PesticideCategory.Id == 0)
            {
                throw new BusinessException("PesticideCategory.NotExisted");
            }

            var pesticideCategory = await pesticideCategoryQueries.GetById(request.PesticideCategory.Id);
            if (pesticideCategory == null)
            {
                throw new BusinessException("PesticideCategory.NotExisted");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.PesticideCategory.CreatedDate = pesticideCategory.CreatedDate;
                        request.PesticideCategory.CreatedBy = pesticideCategory.CreatedBy;
                        request.PesticideCategory = UpdateBuild(request.PesticideCategory, request.LoginSession);
                        request.PesticideCategory.Code = string.IsNullOrWhiteSpace(pesticideCategory.Code)
                            ? (await storageQueries.GenarateCodeAsync(StorageKeys.PesticideCategoryCode))
                            : pesticideCategory.Code;

                        rs = await pesticideCategoryRepository.Update(request.PesticideCategory);

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
