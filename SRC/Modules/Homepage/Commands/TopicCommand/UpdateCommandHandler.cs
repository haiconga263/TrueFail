using Common.Exceptions;
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
	public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
	{
		private readonly ITopicHomepageRepository topicRepository = null;
		private readonly ITopicHomepageQueries topicQueries = null;
		public UpdateCommandHandler(ITopicHomepageRepository topicRepository, ITopicHomepageQueries topicQueries)
		{
			this.topicRepository = topicRepository;
			this.topicQueries = topicQueries;
		}
		public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
		{
			if (request.Topic == null || request.Topic.Id == 0)
			{
				throw new BusinessException("Topic.NotExisted");
			}

			var topic = (await topicQueries.GetTopicById(request.Topic.Id));
			if (topic == null)
			{
				throw new BusinessException("Topic.NotExisted");
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						//request.Topic.CreatedDate = topic.CreatedDate;
						//request.Topic.CreatedBy = topic.CreatedBy;
						request.Topic = UpdateBuild(request.Topic, request.LoginSession);

						rs = await topicRepository.UpdateAsync(request.Topic);

						if (rs != 0)
						{
							return -1;
						}

						//for language
						// languages
						foreach (var item in request.Topic.TopicLanguages)
						{
							item.TopicId = request.Topic.Id;
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
