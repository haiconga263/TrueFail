using Common.Models;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Collections.Repositories
{
    public class CollectionEmployeeRepository : BaseRepository, ICollectionEmployeeRepository
    {

        public async Task<int> Add(CollectionEmployee employee)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(employee);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int collectionEmployeeId)
        {
            var cmd = $@"DELETE FROM `collection_employee` WHERE id = {collectionEmployeeId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(CollectionEmployee employee)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(employee);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
