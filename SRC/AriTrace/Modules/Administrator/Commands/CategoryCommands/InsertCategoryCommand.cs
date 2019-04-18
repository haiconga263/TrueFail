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
    public class InsertCategoryCommand : BaseCommand<int>
    {
        public Category Model { set; get; }
        public InsertCategoryCommand(Category category)
        {
            Model = category;
        }
    }

    public class InsertCategoryCommandHandler : BaseCommandHandler<InsertCategoryCommand, int>
    {
        private readonly ICategoryRepository categoryRepository = null;
        private readonly ICategoryQueries categoryQueries = null;
        public InsertCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryQueries categoryQueries)
        {
            this.categoryRepository = categoryRepository;
            this.categoryQueries = categoryQueries;
        }
        public override async Task<int> HandleCommand(InsertCategoryCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        id = await categoryRepository.AddAsync(request.Model);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (id > 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return id;
        }
    }
}
