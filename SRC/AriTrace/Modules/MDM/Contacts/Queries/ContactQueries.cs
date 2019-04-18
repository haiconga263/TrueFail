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

namespace MDM.Contacts.Queries
{
    public class ContactQueries : BaseQueries, IContactQueries
    {
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `contact` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Contact>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Contact>($"SELECT * FROM `contact` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Contact>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `contact` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Contact>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
