using Common.Exceptions;
using DAL;
using Homepage.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.FaqCommand
{
	public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
	{
		private readonly IFaqHomepageRepository faqRepository = null;
		private readonly IFaqHomepageQueries faqQueries = null;
		public UpdateCommandHandler(IFaqHomepageRepository faqRepository, IFaqHomepageQueries faqQueries)
		{
			this.faqRepository = faqRepository;
			this.faqQueries = faqQueries;
		}
		public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
		{
			if (request.Faq == null || request.Faq.Id == 0)
			{
				throw new BusinessException("Faq.NotExisted");
			}

			var faq = (await faqQueries.GetFaqByIdAsync(request.Faq.Id));
			if (faq == null)
			{
				throw new BusinessException("Faq.NotExisted");
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						request.Faq.CreatedDate = faq.CreatedDate;
						request.Faq.CreatedBy = faq.CreatedBy;
						request.Faq = UpdateBuild(request.Faq, request.LoginSession);

						rs = await faqRepository.UpdateAsync(request.Faq);

						if (rs != 0)
						{
							return -1;
						}

						//for language
						// languages
						foreach (var item in request.Faq.FaqLanguages)
						{
							item.FaqId = request.Faq.Id;
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
