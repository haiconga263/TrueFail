using Common.Models;
using DAL;
using MDM.UI.Addresses.Interfaces;
using MDM.UI.Addresses.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Addresses.Queries
{
    public class AddressQueries : BaseQueries, IAddressQueries
    {
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `address` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Address>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Address>($"SELECT * FROM `address` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Address>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `address` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Address>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
