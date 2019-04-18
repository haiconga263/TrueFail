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
	public class FaqHomepageQueries : BaseQueries, IFaqHomepageQueries
	{
		public async Task<IEnumerable<FaqHomepageViewModel>> GetAllFaq(string condition = "")
		{
			List<FaqHomepageViewModel> result = new List<FaqHomepageViewModel>();
			string cmd = $@"SELECT f.*, fl.* FROM `faq` f
                            LEFT JOIN `faq_language` fl ON f.id = fl.faq_id
                            LEFT JOIN `language` l ON fl.language_id = l.id 
                            WHERE f.is_deleted = 0 ";

			if (!string.IsNullOrWhiteSpace(condition))
			{
				cmd += " AND " + condition;
			}
			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Faq, FaqLanguage, FaqHomepageViewModel>(
					(fRs, flRs) =>
					{
						var faq = CommonHelper.Mapper<Faq, FaqHomepageViewModel>(fRs);
						var item = result.FirstOrDefault(x => x.Id == faq.Id);
						if (item == null)
						{
							result.Add(faq);
						}

						if (flRs != null)
						{
							
							var faqLang = faq.FaqLanguages.FirstOrDefault(l => l.Id == flRs.Id);
							if (faqLang == null)
							{
								faq.FaqLanguages.Add(flRs);
							}
							faq.QuestionDisplay = string.IsNullOrWhiteSpace(flRs.Question) ? faq.Question : flRs.Question;
							faq.AnswerDisplay = string.IsNullOrWhiteSpace(flRs.Answer) ? faq.Answer : flRs.Answer;
						}

						return faq;
					}
				);

				return result;
			}
			else
			{
				using (var conn = DALHelper.GetConnection())
				{
					var rd = await conn.QueryMultipleAsync(cmd);
					rd.Read<Faq, FaqLanguage, FaqHomepageViewModel>(
					(fRs, flRs) =>
					{
						var faq = CommonHelper.Mapper<Faq, FaqHomepageViewModel>(fRs);
						var item = result.FirstOrDefault(x => x.Id == faq.Id);
						if (item == null)
						{
							result.Add(faq);
						}

						if (flRs != null)
						{
							var faqLang = faq.FaqLanguages.FirstOrDefault(l => l.Id == flRs.Id);
							if (faqLang == null)
							{
								faq.FaqLanguages.Add(flRs);
							}
							faq.QuestionDisplay = string.IsNullOrWhiteSpace(flRs.Question) ? faq.Question : flRs.Question;
							faq.AnswerDisplay = string.IsNullOrWhiteSpace(flRs.Answer) ? faq.Answer : flRs.Answer;
						}

						return faq;
					}
				);

					return result;
				}
			}
		}

		public async Task<FaqHomepageViewModel> GetFaqByIdAsync(int faqId)
		{
			FaqHomepageViewModel result = null;
			string cmd = $@"SELECT * FROM `faq` f
                            LEFT JOIN `faq_language` fl ON f.id = fl.faq_id
                            WHERE f.is_deleted = 0 and f.id = {faqId}";

			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Faq, FaqLanguage, FaqHomepageViewModel>(
					(fRs, flRs) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Faq, FaqHomepageViewModel>(fRs);
						}


						if (flRs != null)
						{

							result.FaqLanguages.Add(flRs);

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
					rd.Read<Faq, FaqLanguage, FaqHomepageViewModel>(
					(fRs, flRs) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Faq, FaqHomepageViewModel>(fRs);
						}

						if (flRs != null)
						{

							result.FaqLanguages.Add(flRs);

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
