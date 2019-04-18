using DAL;
using Homepage.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.TopicCommand
{
	public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
	{
		private readonly ITopicHomepageRepository topicRepository = null;
		public AddCommandHandler(ITopicHomepageRepository topicRepository)
		{
			this.topicRepository = topicRepository;
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
						request.Topic = CreateBuild(request.Topic, request.LoginSession);
						request.Topic.IsUsed = true;
						var topicId = await topicRepository.AddAsync(request.Topic);

						// languages
						foreach (var item in request.Topic.TopicLanguages)
						{
							item.TopicId = topicId;
							await topicRepository.AddOrUpdateLanguage(item);
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
