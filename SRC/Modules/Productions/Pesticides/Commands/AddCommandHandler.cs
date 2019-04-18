using Common.Exceptions;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Pesticides.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IPesticideRepository pesticideRepository = null;
        private readonly IPesticideQueries pesticideQueries = null;
        private readonly IPesticideCategoryQueries pesticideCategoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(IPesticideRepository pesticideRepository,
            IPesticideQueries pesticideQueries,
            IPesticideCategoryQueries pesticideCategoryQueries,
            IStorageQueries storageQueries)
        {
            this.pesticideRepository = pesticideRepository;
            this.pesticideQueries = pesticideQueries;
            this.pesticideCategoryQueries = pesticideCategoryQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if ((request.Pesticide?.CategoryId ?? 0) > 0)
            {
                var category = await pesticideCategoryQueries.GetById(request.Pesticide.CategoryId ?? 0);
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
                        pesticideRepository.JoinTransaction(conn, trans);
                        pesticideQueries.JoinTransaction(conn, trans);

                        if (request.Pesticide == null)
                        {
                            throw new BusinessException("AddWrongInformation");
                        }

                        request.Pesticide.Code = await storageQueries.GenarateCodeAsync(StorageKeys.PesticideCode);
                        request.Pesticide = CreateBuild(request.Pesticide, request.LoginSession);
                        var pesticideId = await pesticideRepository.Add(request.Pesticide);

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
