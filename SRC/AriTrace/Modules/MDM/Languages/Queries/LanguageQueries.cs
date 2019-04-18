using Common.Models;
using DAL;
using MDM.UI.Languages.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Languages.Queries
{
    public class LanguageQueries : BaseQueries, ILanguageQueries
    {
        public async Task<IEnumerable<UI.Languages.Models.Language>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `language` WHERE `is_deleted` = 0";
            return await DALHelper.Query<UI.Languages.Models.Language>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<UI.Languages.Models.Language> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<UI.Languages.Models.Language>($"SELECT * FROM `language` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<UI.Languages.Models.Language>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `language` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<UI.Languages.Models.Language>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
