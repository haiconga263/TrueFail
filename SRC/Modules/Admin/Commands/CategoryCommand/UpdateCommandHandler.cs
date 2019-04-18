using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Categories.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.CategoryCommand
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ICategoryRepository categoryRepository = null;
        private readonly ICategoryQueries categoryQueries = null;
        public UpdateCommandHandler(ICategoryRepository categoryRepository, ICategoryQueries categoryQueries)
        {
            this.categoryRepository = categoryRepository;
            this.categoryQueries = categoryQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Category == null || request.Category.Id == 0)
            {
                throw new BusinessException("Category.NotExisted");
            }

            var category = await categoryQueries.GetById(request.Category.Id);
            if (category == null)
            {
                throw new BusinessException("Category.NotExisted");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Category.CreatedDate = category.CreatedDate;
                        request.Category.CreatedBy = category.CreatedBy;
                        request.Category = UpdateBuild(request.Category, request.LoginSession);
                        //request.Category.Code = category.Code;
                        rs = await categoryRepository.Update(request.Category);

                        if (rs == 0)
                        {
                            return -1;
                        }

                        //for language
                        // languages
                        foreach (var item in request.Category.Languages)
                        {
                            item.CategoryId = request.Category.Id;
                            await categoryRepository.AddOrUpdateLanguage(item);
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
