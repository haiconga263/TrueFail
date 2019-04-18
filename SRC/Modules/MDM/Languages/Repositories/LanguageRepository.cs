using Common.Models;
using DAL;
using MDM.UI.Languages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Languages.Repositories
{
    public class LanguageRepository : BaseRepository, ILanguageRepository
    {
        public async Task<int> AddCaptionLanguage(CaptionLanguage language)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(language);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteCaptionLanguages(int captionId)
        {
            string cmd = $"Delete from `caption_language` WHERE caption_id = {captionId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs > 0 ? 0 : -1;
        }

        public async Task<int> UpdateCaption(Caption caption)
        {
            {
                var cmd = QueriesCreatingHelper.CreateQueryUpdate(caption);
                var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
                return rs == 0 ? -1 : 0;
            }
        }
    }
}
