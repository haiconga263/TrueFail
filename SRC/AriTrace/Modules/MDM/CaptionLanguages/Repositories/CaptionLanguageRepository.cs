using Common.Models;
using DAL;
using MDM.UI.CaptionLanguages.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.CaptionLanguages.Repositories
{
    public class CaptionLanguageRepository : BaseRepository, ICaptionLanguageRepository
    {
        public async Task<int> AddAsync(UI.CaptionLanguages.Models.CaptionLanguage captionLanguage)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(captionLanguage);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `caption_language` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(UI.CaptionLanguages.Models.CaptionLanguage captionLanguage)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(captionLanguage);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
