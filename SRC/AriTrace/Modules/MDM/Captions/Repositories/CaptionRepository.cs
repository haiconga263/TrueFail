using Common.Models;
using DAL;
using MDM.UI.Captions.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Captions.Repositories
{
    public class CaptionRepository : BaseRepository, ICaptionRepository
    {
        public async Task<int> AddAsync(UI.Captions.Models.Caption caption)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(caption);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `caption` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(UI.Captions.Models.Caption caption)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(caption);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
