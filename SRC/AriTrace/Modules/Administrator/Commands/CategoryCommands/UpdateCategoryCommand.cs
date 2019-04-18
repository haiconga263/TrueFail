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
    public class UpdateCategoryCommand : BaseCommand<int>
    {
        public Category Model { set; get; }
        public UpdateCategoryCommand(Category category)
        {
            Model = category;
        }
    }

    public class UpdateCategoryCommandHandler : BaseCommandHandler<UpdateCategoryCommand, int>
    {
        private readonly ICategoryRepository categoryRepository = null;
        private readonly ICategoryQueries categoryQueries = null;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryQueries categoryQueries)
        {
            this.categoryRepository = categoryRepository;
            this.categoryQueries = categoryQueries;
        }
        public override async Task<int> HandleCommand(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Category.NotExisted");
            }
            else
            {
                category = await categoryQueries.GetByIdAsync(request.Model.Id);
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
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        if (await categoryRepository.UpdateAsync(request.Model) > 0)
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
