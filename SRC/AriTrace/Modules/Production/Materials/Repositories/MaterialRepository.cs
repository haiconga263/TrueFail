using Common.Models;
using DAL;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Materials.Repositories
{
    public class MaterialRepository : BaseRepository, IMaterialRepository
    {
        public async Task<int> AddAsync(Material material)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(material);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `material` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Material material)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(material);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
