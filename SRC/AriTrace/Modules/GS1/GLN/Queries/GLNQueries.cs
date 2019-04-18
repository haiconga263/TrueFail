using Common.Models;
using DAL;
using Dapper;
using GS1.UI.GLN.Interfaces;
using GS1.UI.GLN.Mappings;
using GS1.UI.GLN.Models;
using GS1.UI.GLN.ViewModels;
using MDM.UI.Companies.Models;
using MDM.UI.Countries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1.GLN.Queries
{
	public class GLNQueries : BaseQueries, IGLNQueries
	{
		public async Task<IEnumerable<GLNModel>> GetAllAsync()
		{
			string cmd = $@"SELECT gln.*, com.*, cou.* FROM `gln` gln
                            LEFT JOIN `company` com ON gln.partner_id = com.id
                            LEFT JOIN `country` cou ON gln.country_id = cou.id
                            Where gln.`is_deleted` = 0";
			var conn = DbConnection;

			if (conn == null)
				conn = DALHelper.GetConnection();
			try
			{
				using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
				{
					return reader.Read<GLNModel, Company, Country,GLNDetailViewModel>(
						(glnRs, companyRs, countryRs) =>
						{
							GLNDetailViewModel gln = null;
							if (glnRs != null)
							{
								gln = glnRs.ToInformation();
							}
							else
							{
								gln = new GLNDetailViewModel();
							}
							if (companyRs != null)
							{
								gln.Company = companyRs;
							}
							if (countryRs != null)
							{
								gln.Country = countryRs;
							}

							return gln;
						}
					);
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				if (DbConnection == null) conn.Dispose();
			}
		}

		public async Task<GLNModel> GetByIdAsync(int id)
		{
			return (await DALHelper.Query<GLNModel>($"SELECT * FROM `gln` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
				.FirstOrDefault();
		}

		public async Task<IEnumerable<GLNModel>> GetsAsync()
		{
			string cmd = $"SELECT * FROM `gln` WHERE `is_used` = 1";
			return await DALHelper.Query<GLNModel>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
		}

		public async Task<IEnumerable<GLNModel>> GetCodeAsyncByPartner(int gS1Code)
		{
			string cmd = $"SELECT * FROM `gln` WHERE `is_used` = 1 AND `partner_code` = '{gS1Code}'";
			return await DALHelper.Query<GLNModel>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
		}

	}
}
