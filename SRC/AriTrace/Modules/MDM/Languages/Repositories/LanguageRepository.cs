using Common.Models;
using DAL;
using MDM.UI.Languages.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Languages.Repositories
{
    public class LanguageRepository : BaseRepository, ILanguageRepository
    {
        public async Task<int> AddAsync(UI.Languages.Models.Language language)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(language);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `language` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(UI.Languages.Models.Language language)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(language);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
