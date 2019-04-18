using Common.Models;
using DAL;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GS1.GTINs.Repositories
{
    public class GTINRepository : BaseRepository, IGTINRepository
    {
        public async Task<int> AddAsync(GTIN gTIN)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(gTIN);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `gtin` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(GTIN gTIN)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(gTIN);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
