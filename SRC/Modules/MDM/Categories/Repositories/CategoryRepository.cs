using Common.Models;
using DAL;
using MDM.UI.Categories.Interfaces;
using MDM.UI.Categories.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Categories.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public async Task<int> Add(Category category)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(category);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddOrUpdateLanguage(CategoryLanguage language)
        {
            string cmd = QueriesCreatingHelper.CreateQuerySelect<CategoryLanguage>($"category_id = {language.CategoryId} AND language_id = {language.LanguageId}");
            var lang = (await DALHelper.ExecuteQuery<CategoryLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
            if (lang == null)
            {
                cmd = QueriesCreatingHelper.CreateQueryInsert(language);
                cmd += ";SELECT LAST_INSERT_ID();";
                return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            }
            else
            {
                language.Id = lang.Id;
                cmd = QueriesCreatingHelper.CreateQueryUpdate(language);
                var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
                return rs == 0 ? -1 : language.Id;
            }
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `category` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(Category category)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(category);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}