using DAL;
using Homepage.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.PageCommand
{
	public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
	{
		private readonly IPageHomepageRepository pageRepository = null;
		public AddCommandHandler(IPageHomepageRepository pageRepository)
		{
			this.pageRepository = pageRepository;
		}
		public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
		{
			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						request.Page = CreateBuild(request.Page, request.LoginSession);
						request.Page.IsUsed = true;
						var pageId = await pageRepository.AddAsync(request.Page);

						// languages
						foreach (var item in request.Page.PageLanguages)
						{
							item.PageId = pageId;
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
