using Common.Models;
using DAL;
using MDM.UI.CaptionLanguages.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.CaptionLanguages.Queries
{
    public class CaptionLanguageQueries : BaseQueries, ICaptionLanguageQueries
    {
        public async Task<IEnumerable<UI.CaptionLanguages.Models.CaptionLanguage>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `caption_language` WHERE `is_deleted` = 0";
            return await DALHelper.Query<UI.CaptionLanguages.Models.CaptionLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<UI.CaptionLanguages.Models.CaptionLanguage> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<UI.CaptionLanguages.Models.CaptionLanguage>($"SELECT * FROM `caption_language` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<UI.CaptionLanguages.Models.CaptionLanguage>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `caption_language` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<UI.CaptionLanguages.Models.CaptionLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
