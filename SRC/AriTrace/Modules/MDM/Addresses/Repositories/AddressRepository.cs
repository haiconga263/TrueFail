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

namespace MDM.Addresses.Repositories
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        public async Task<int> AddAsync(Address address)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(address);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `address` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Address address)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(address);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
