using Common.Exceptions;
using DAL;
using MDM.UI.Categories.Interfaces;
using MDM.UI.Categories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteCategoryCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteCategoryCommandHandler : BaseCommandHandler<DeleteCategoryCommand, int>
    {
        private readonly ICategoryRepository categoryRepository = null;
        private readonly ICategoryQueries categoryQueries = null;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryQueries categoryQueries)
        {
            this.categoryRepository = categoryRepository;
            this.categoryQueries = categoryQueries;
        }
        public override async Task<int> HandleCommand(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Category.NotSelected");
            }
            else
            {
                category = await categoryQueries.GetByIdAsync(request.Model);
                if (category == null)
                    throw new BusinessException("Category.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        category.IsDeleted = true;
                        category.ModifiedDate = DateTime.Now;
                        category.ModifiedBy = request.LoginSession.Id;

                        if (await categoryRepository.UpdateAsync(category) > 0)
                            rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return rs;
        }
    }
}
