using Common.Exceptions;
using DAL;
using Homepage.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.PageCommand
{
	public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
	{
		private readonly IPageHomepageRepository pageRepository = null;
		private readonly IPageHomepageQueries pageQueries = null;
		public UpdateCommandHandler(IPageHomepageRepository pageRepository, IPageHomepageQueries pageQueries)
		{
			this.pageRepository = pageRepository;
			this.pageQueries = pageQueries;
		}
		public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
		{
			if (request.Page == null || request.Page.Id == 0)
			{
				throw new BusinessException("Page.NotExisted");
			}

			var page = (await pageQueries.GetPageByIdAsync(request.Page.Id));
			if (page == null)
			{
				throw new BusinessException("Page.NotExisted");
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						request.Page = UpdateBuild(request.Page, request.LoginSession);

						rs = await pageRepository.UpdateAsync(request.Page);

						if (rs != 0)
						{
							return -1;
						}

						//for language
						// languages
						foreach (var item in request.Page.PageLanguages)
						{
							item.PageId = request.Page.Id;
							await pageRepository.AddOrUpdateLanguage(item);
						}


						rs = 0;
					}
					catch (Exception ex)
					{
						throw ex;
					}
					finally
					{
						if (rs == 0)
						{
							trans.Commit();
						}
						else
						{
							try
							{
								trans.Rollback();
							}
							catch { }
						}
					}
				}
			}

			return rs;
		}
	}
}
