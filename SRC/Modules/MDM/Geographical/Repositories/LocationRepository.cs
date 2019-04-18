using Common.Models;
using DAL;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Geographical.Repositories
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        public async Task<int> AddOrUpdateAddress(Address address)
        {
            var cmd = string.Empty;
            if (address.Id != 0)
            {
                cmd = QueriesCreatingHelper.CreateQueryUpdate(address);
                var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
                return rs == 0 ? -1 : address.Id;
            }
            else
            {
                cmd = QueriesCreatingHelper.CreateQueryInsert(address);
                cmd += ";SELECT LAST_INSERT_ID();";
                return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
            }
        }

        public async Task<int> AddOrUpdateContact(Contact contact)
        {
            var cmd = string.Empty;
            if (contact.Id != 0)
            {
                cmd = QueriesCreatingHelper.CreateQueryUpdate(contact);
                var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
                return rs == 0 ? -1 : contact.Id;
            }
            else
            {
                cmd = QueriesCreatingHelper.CreateQueryInsert(contact);
                cmd += ";SELECT LAST_INSERT_ID();";
                return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
            }
        }
    }
}
