using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Categories;
using MDM.UI.Categories.Interfaces;
using MDM.UI.Categories.Models;
using MDM.UI.Categories.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Categories.Queries
{
    public class CategoryQueries : BaseQueries, ICategoryQueries
    {
        public async Task<IEnumerable<Category>> GetAll(string lang = "vi")
        {
            string cmd = $@"SELECT c.`id`, c.`code`, coalesce(cpl.`caption`, c.`default_name`) as 'default_name', c.`caption_name_id`, c.`is_used`, 
		                            c.`parent_id`, c.`is_deleted`, c.`created_date`, c.`created_by`, c.`modified_date`, c.`modified_by`
                            FROM category c
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `caption_language` cpl ON cpl.language_id = l.id AND cpl.caption_id = c.caption_name_id
                            WHERE c.`is_deleted` = 0";
            return await DALHelper.Query<Category>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<CategoryLanguageViewModel> GetById(int id)
        {
            string cmd = $@"SELECT c.*, cl.* FROM `category` c
	                        LEFT JOIN `category_language` cl ON c.id = cl.category_id 
	                        WHERE c.id = '{id}' and c.is_deleted = 0";

            List<CategoryLanguageViewModel> result = new List<CategoryLanguageViewModel>();
            DbConnection = DbConnection ?? DALHelper.GetConnection();
            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<Category, CategoryLanguage, CategoryLanguageViewModel>(
                (categoryRs, categoryLanguageRs) =>
                {
                    var category = CommonHelper.Mapper<Category, CategoryLanguageViewModel>(categoryRs);
                    result.Add(category);

                    if (categoryLanguageRs != null)
                    {
                        var lang = category.Languages.FirstOrDefault(l => l.Id == categoryLanguageRs.Id);
                        if (lang == null)
                        {
                            category.Languages.Add(categoryLanguageRs);
                        }
                    }
                    else
                    {
                        category.Name = category.Name;
                    }

                    return category;
                }
            );

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Category>> Gets(string lang = "vi")
        {
            string cmd = $@"SELECT c.`id`, c.`code`, coalesce(cpl.`caption`, c.`default_name`) as 'default_name', c.`caption_name_id`, c.`is_used`, 
		                            c.`parent_id`, c.`is_deleted`, c.`created_date`, c.`created_by`, c.`modified_date`, c.`modified_by`
                            FROM category c
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `caption_language` cpl ON cpl.language_id = l.id AND cpl.caption_id = c.caption_name_id
                            WHERE c.`is_deleted` = 0 AND c.`is_used` = 1 ";
            return await DALHelper.Query<Category>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetsWithChild(string lang = "vi")
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            var categories = (await Gets(lang)).ToList();
            return ProcessHierarchy(categories, null);
        }

        private IEnumerable<CategoryViewModel> ProcessHierarchy(IEnumerable<Category> categories, int? parentId)
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            foreach (var category in categories.Where(c => c.ParentId == parentId))
            {
                var viewModel = CommonHelper.Mapper<Category, CategoryViewModel>(category);
                viewModel.Childs = ProcessHierarchy(categories, viewModel.Id);
                result.Add(viewModel);
            }
            return result;
        }
    }
}
