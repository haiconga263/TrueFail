using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Queries
{
	public class ContactHomepageQueries : BaseQueries, IContactHomepageQueries
	{
		public async Task<IEnumerable<ContactHomepageViewModel>> GetAllContact()
		{
			string cmd = $@"SELECT * FROM `hp_contact`
                            WHERE is_deleted = 0 ";
			var result = new List<ContactHomepageViewModel>();

			var contact = await DALHelper.Query<Contact>(cmd, dbTransaction: DbTransaction, connection: DbConnection);

			if (contact != null)
			{
				foreach (var item in contact)
				{
					var cvm = CommonHelper.Mapper<Contact, ContactHomepageViewModel>(item);
					result.Add(cvm);
				}
			}
			return result;
		}

		public async Task<ContactHomepageViewModel> GetContactById(int contactId, int languageId = 1)
		{
			ContactHomepageViewModel rs = new ContactHomepageViewModel();

			var contactItem = (await DALHelper.Query<Contact>($"SELECT * FROM `hp_contact` WHERE `id` = {contactId}  AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();

			if (contactItem != null)
			{
				rs = CommonHelper.Mapper<Contact, ContactHomepageViewModel>(contactItem);
			}

			return rs;
		}
	}
}

