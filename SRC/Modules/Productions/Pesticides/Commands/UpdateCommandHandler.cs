using Common.Exceptions;
using DAL;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Pesticides.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Pesticides.Commands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IPesticideRepository pesticideRepository = null;
        private readonly IPesticideQueries pesticideQueries = null;
        private readonly IPesticideCategoryQueries pesticideCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;
        public UpdateCommandHandler(IPesticideRepository pesticideRepository,
            IPesticideQueries pesticideQueries,
            IPesticideCategoryQueries pesticideCategoryQueries,
            IStorageQueries storageQueries)
        {
            this.pesticideRepository = pesticideRepository;
            this.pesticideQueries = pesticideQueries;
            this.pesticideCategoryQueries = pesticideCategoryQueries;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Pesticide == null || request.Pesticide.Id == 0)
            {
                throw new BusinessException("Pesticide.NotExisted");
            }

            var pesticide = await pesticideQueries.GetById(request.Pesticide.Id);
            if (pesticide == null)
            {
                throw new BusinessException("Pesticide.NotExisted");
            }

            if ((request.Pesticide?.CategoryId ?? 0) > 0)
            {
                var category = await pesticideCategoryQueries.GetById(request.Pesticide.CategoryId ?? 0);
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
                        request.Pesticide.CreatedDate = pesticide.CreatedDate;
                        request.Pesticide.CreatedBy = pesticide.CreatedBy;
                        request.Pesticide = UpdateBuild(request.Pesticide, request.LoginSession);
                        request.Pesticide.Code = string.IsNullOrWhiteSpace(pesticide.Code)
                            ? (await storageQueries.GenarateCodeAsync(StorageKeys.PesticideCode))
                            : pesticide.Code;

                        rs = await pesticideRepository.Update(request.Pesticide);

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
