using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.TopicCommand
{
	public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
	{
		private readonly ITopicHomepageRepository topicRepository = null;
		public DeleteCommandHandler(ITopicHomepageRepository topicRepository)
		{
			this.topicRepository = topicRepository;
		}
		public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
		{
			return await topicRepository.DeleteAsync(DeleteBuild(new Topic()
			{
				Id = request.TopicId
			}, request.LoginSession));
		}
	}
}
