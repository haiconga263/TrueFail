using Common.Models;
using DAL;
using Productions.UI.Methods.Interfaces;
using Productions.UI.Methods.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Methods.Repositories
{
    public class MethodRepository : BaseRepository, IMethodRepository
    {
        public async Task<int> Add(Method method)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(method);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{Method.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(Method method)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(method);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}