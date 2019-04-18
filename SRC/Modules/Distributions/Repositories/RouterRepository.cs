using Common.Models;
using DAL;
using Distributions.UI.Interfaces;
using Distributions.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.Repositories
{
    public class RouterRepository : BaseRepository, IRouterRepository
    {
        public async Task<int> Create(Router router)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(router);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Router router)
        {
            var cmd = $@"UPDATE `router`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {router.ModifiedBy},
                         `modified_date` = '{router.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {router.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Router router)
        { 
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(router);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
