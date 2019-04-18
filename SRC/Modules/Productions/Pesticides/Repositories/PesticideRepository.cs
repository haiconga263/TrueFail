using Common.Models;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Pesticides.Repositories
{
    public class PesticideRepository : BaseRepository, IPesticideRepository
    {
        public async Task<int> Add(Pesticide pesticide)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(pesticide);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{Pesticide.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(Pesticide pesticide)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(pesticide);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}