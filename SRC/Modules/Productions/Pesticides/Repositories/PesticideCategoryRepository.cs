using Common.Models;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Pesticides.Repositories
{
    public class PesticideCategoryRepository : BaseRepository, IPesticideCategoryRepository
    {
        public async Task<int> Add(PesticideCategory pesticideCategory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(pesticideCategory);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }


        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{PesticideCategory.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(PesticideCategory pesticideCategory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(pesticideCategory);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}