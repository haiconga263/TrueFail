using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using Homepage.UI.Interfaces;
using MDM.UI;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.TopicPostCommand
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
		private readonly ITopicPostHomepageRepository topicPostRepository = null;
		private readonly ITopicPostHomepageQueries topicPostQueries = null;
		public AddCommandHandler(ITopicPostHomepageRepository topicPostRepository, ITopicPostHomepageQueries topicPostQueries)
        {
            this.topicPostRepository = topicPostRepository;
            this.topicPostQueries = topicPostQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.TopicPost == null || request.TopicPost.TopicId == 0 || request.TopicPost.PostId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var checkPost = (await topicPostQueries.Gets($"topic_id = {request.TopicPost.TopicId} and post_id = {request.TopicPost.PostId}")).FirstOrDefault();
            if(checkPost != null)
            {
                throw new BusinessException("Post.NotExisted");
            }

            return await topicPostRepository.Add(request.TopicPost) > 0 ? 0 : throw new BusinessException("Common.AddFail");
        }
    }
}
