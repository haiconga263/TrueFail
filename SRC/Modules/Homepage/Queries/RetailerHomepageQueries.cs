using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Dapper;
using DAL;
using Homepage.UI.Interfaces;
using Homepage.UI.ViewModels;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Models;

namespace Homepage.Queries
{
	public class RetailerHomepageQueries : BaseQueries, IRetailerHomepageQueries
	{
		
		public async Task<IEnumerable<RetailerHomepageViewModel>> GetRetailerAsync(string condition = "")
		{
			List<RetailerHomepageViewModel> result = new List<RetailerHomepageViewModel>();
			string cmd = $@"SELECT * FROM `retailer` r 
                            LEFT JOIN `address` a ON r.address_id = a.id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON r.contact_id = c.id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE r.is_used = 1 AND r.is_deleted = 0";
			if (!string.IsNullOrEmpty(condition))
			{
				cmd += " AND " + condition;
			}
			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Retailer, Address, Contact, RetailerHomepageViewModel>(
					(rRs, aRs, cRs) =>
					{
						var retailer = result.FirstOrDefault(r => r.Id == rRs.Id);
						if (retailer == null)
						{
							retailer = CommonHelper.Mapper<Retailer, RetailerHomepageViewModel>(rRs);
							result.Add(retailer);
						}

						if (retailer.Address == null)
						{
							retailer.Address = aRs;
						}

						if (retailer.Contact == null)
						{
							retailer.Contact = cRs;
						}

						return retailer;
					}
				);

				return result;
			}
			else
			{
				using (var conn = DALHelper.GetConnection())
				{
					var rd = await conn.QueryMultipleAsync(cmd);
					rd.Read<Retailer, Address, Contact, RetailerHomepageViewModel>(
						(rRs, aRs, cRs) =>
						{
							var retailer = result.FirstOrDefault(r => r.Id == rRs.Id);
							if (retailer == null)
							{
								retailer = CommonHelper.Mapper<Retailer, RetailerHomepageViewModel>(rRs);
								result.Add(retailer);
							}

							if (retailer.Address == null)
							{
								retailer.Address = aRs;
							}

							if (retailer.Contact == null)
							{
								retailer.Contact = cRs;
							}

							return retailer;
						}
					);

					return result;
				}
			}
		}
	}
}
