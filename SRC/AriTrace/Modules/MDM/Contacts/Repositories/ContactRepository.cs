using Common.Models;
using DAL;
using MDM.UI.Contacts.Interfaces;
using MDM.UI.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Contacts.Repositories
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public async Task<int> AddAsync(Contact contact)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(contact);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `contact` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Contact contact)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(contact);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
