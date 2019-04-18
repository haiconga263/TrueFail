using Common.Models;
using DAL;
using MDM.UI.Roles.Interfaces;
using MDM.UI.Roles.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Roles.Queries
{
    public class RoleQueries : BaseQueries, IRoleQueries
    {
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `role` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Role>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Role>($"SELECT * FROM `role` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Role>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `role` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Role>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
