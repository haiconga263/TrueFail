using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Queries
{
	public class PageHomepageQueries : BaseQueries, IPageHomepageQueries
	{
		public async Task<IEnumerable<PageHomepageViewModel>> GetAllPage(string condition = "")
		{
			List<PageHomepageViewModel> result = new List<PageHomepageViewModel>();
			string cmd = $@"SELECT f.*, fl.* FROM `page` f
                            LEFT JOIN `page_language` fl ON f.id = fl.page_id
                            LEFT JOIN `language` l ON fl.language_id = l.id 
                            WHERE f.is_deleted = 0 ";

			if (!string.IsNullOrWhiteSpace(condition))
			{
				cmd += " AND " + condition;
			}
			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Page, PageLanguage, PageHomepageViewModel>(
					(fRs, flRs) =>
					{
						var page = CommonHelper.Mapper<Page, PageHomepageViewModel>(fRs);
						var item = result.FirstOrDefault(x => x.Id == page.Id);
						if (item == null)
						{
							result.Add(page);
						}

						if (flRs != null)
						{
							
							var pageLang = page.PageLanguages.FirstOrDefault(l => l.Id == flRs.Id);
							if (pageLang == null)
							{
								page.PageLanguages.Add(flRs);
							}
							page.TitleDisplay = string.IsNullOrWhiteSpace(flRs.Title) ? page.Title : flRs.Title;
							page.ContentDisplay = string.IsNullOrWhiteSpace(flRs.Content) ? page.Content : flRs.Content;
						}

						return page;
					}
				);

				return result;
			}
			else
			{
				using (var conn = DALHelper.GetConnection())
				{
					var rd = await conn.QueryMultipleAsync(cmd);
					rd.Read<Page, PageLanguage, PageHomepageViewModel>(
					(fRs, flRs) =>
					{
						var page = CommonHelper.Mapper<Page, PageHomepageViewModel>(fRs);
						var item = result.FirstOrDefault(x => x.Id == page.Id);
						if (item == null)
						{
							result.Add(page);
						}

						if (flRs != null)
						{
							var pageLang = page.PageLanguages.FirstOrDefault(l => l.Id == flRs.Id);
							if (pageLang == null)
							{
								page.PageLanguages.Add(flRs);
							}
							page.TitleDisplay = string.IsNullOrWhiteSpace(flRs.Title) ? page.Title : flRs.Title;
							page.ContentDisplay = string.IsNullOrWhiteSpace(flRs.Content) ? page.Content : flRs.Content;
						}

						return page;
					}
				);

					return result;
				}
			}
		}

		public async Task<PageHomepageViewModel> GetPageByIdAsync(int pageId)
		{
			PageHomepageViewModel result = null;
			string cmd = $@"SELECT * FROM `page` f
                            LEFT JOIN `page_language` fl ON f.id = fl.page_id
                            WHERE f.is_deleted = 0 and f.id = {pageId}";

			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Page, PageLanguage, PageHomepageViewModel>(
					(fRs, flRs) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Page, PageHomepageViewModel>(fRs);
						}


						if (flRs != null)
						{

							result.PageLanguages.Add(flRs);

						}

						return result;
					}
				);

				return result;
			}
			else
			{
				using (var conn = DALHelper.GetConnection())
				{
					var rd = await conn.QueryMultipleAsync(cmd);
					rd.Read<Page, PageLanguage, PageHomepageViewModel>(
					(fRs, flRs) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Page, PageHomepageViewModel>(fRs);
						}

						if (flRs != null)
						{

							result.PageLanguages.Add(flRs);

						}

						return result;
					}
				);

					return result;
				}
			}
		}
	}
}
