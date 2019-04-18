using Common.Exceptions;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Pesticides.Commands.PesticideCategories
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IPesticideCategoryRepository pesticideCategoryRepository = null;
        private readonly IPesticideCategoryQueries pesticideCategoryQueries = null;

        public DeleteCommandHandler(IPesticideCategoryRepository pesticideCategoryRepository, IPesticideCategoryQueries pesticideCategoryQueries)
        {
            this.pesticideCategoryRepository = pesticideCategoryRepository;
            this.pesticideCategoryQueries = pesticideCategoryQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var pesticideCategory = await pesticideCategoryQueries.GetById(request.Id);
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

                        var categories = (await pesticideCategoryQueries.GetAll()).Where(x => x.ParentId == pesticideCategory.Id).ToList();

                        for (int i = 0; i < categories.Count; i++)
                        {
                            categories[i].ParentId = null;
                            categories[i] = UpdateBuild(categories[i], request.LoginSession);
                            await pesticideCategoryRepository.Update(categories[i]);
                        }

                        pesticideCategory.IsDeleted = true;
                        pesticideCategory = UpdateBuild(pesticideCategory, request.LoginSession);

                        if (await pesticideCategoryRepository.Update(pesticideCategory) > 0) rs = 0;
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
