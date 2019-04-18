using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Categories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;
using MDM.UI.Categories.Models;
using Common.Exceptions;
using System.Linq;
using DAL;

namespace Admin.Commands.CategoryCommand
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ICategoryRepository categoryRepository = null;
        private readonly ICategoryQueries categoryQueries = null;

        public DeleteCommandHandler(ICategoryRepository categoryRepository, ICategoryQueries categoryQueries)
        {
            this.categoryRepository = categoryRepository;
            this.categoryQueries = categoryQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryQueries.GetById(request.CategoryId);
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

                        var categories = (await categoryQueries.GetAll()).Where(x => x.ParentId == category.Id).ToList();

                        for (int i = 0; i < categories.Count; i++)
                        {
                            categories[i].ParentId = null;
                            categories[i] = UpdateBuild(categories[i], request.LoginSession);
                            await categoryRepository.Update(categories[i]);
                        }

                        category.IsDeleted = true;
                        category = UpdateBuild(category, request.LoginSession);

                        if (await categoryRepository.Update(category) > 0) rs = 0;
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
