using DAL;
using Homepage.Commands.ContactCommand;
using Homepage.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.FaqCommand
{
	public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
	{
		private readonly IFaqHomepageRepository faqRepository = null;
		public AddCommandHandler(IFaqHomepageRepository faqRepository)
		{
			this.faqRepository = faqRepository;
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
						request.Faq = CreateBuild(request.Faq, request.LoginSession);
						request.Faq.IsUsed = true;
						var faqId = await faqRepository.AddAsync(request.Faq);

						// languages
						foreach (var item in request.Faq.FaqLanguages)
						{
							item.FaqId = faqId;
							await faqRepository.AddOrUpdateLanguage(item);
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
