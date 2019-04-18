using Common.Exceptions;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Productions.Fertilizers.Commands.FertilizerCategories
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IFertilizerCategoryRepository fertilizerCategoryRepository = null;
        private readonly IFertilizerCategoryQueries fertilizerCategoryQueries = null;

        public DeleteCommandHandler(IFertilizerCategoryRepository fertilizerCategoryRepository, IFertilizerCategoryQueries fertilizerCategoryQueries)
        {
            this.fertilizerCategoryRepository = fertilizerCategoryRepository;
            this.fertilizerCategoryQueries = fertilizerCategoryQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var fertilizerCategory = await fertilizerCategoryQueries.GetById(request.Id);
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

                        var categories = (await fertilizerCategoryQueries.GetAll()).Where(x => x.ParentId == fertilizerCategory.Id).ToList();

                        for (int i = 0; i < categories.Count; i++)
                        {
                            categories[i].ParentId = null;
                            categories[i] = UpdateBuild(categories[i], request.LoginSession);
                            await fertilizerCategoryRepository.Update(categories[i]);
                        }

                        fertilizerCategory.IsDeleted = true;
                        fertilizerCategory = UpdateBuild(fertilizerCategory, request.LoginSession);

                        if (await fertilizerCategoryRepository.Update(fertilizerCategory) > 0) rs = 0;
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
