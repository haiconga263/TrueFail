using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using Homepage.UI.Interfaces;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.TopicPostCommand
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ITopicPostHomepageRepository topicPostRepository = null;
        private readonly ITopicPostHomepageQueries topicPostQueries = null;
        public UpdateCommandHandler(ITopicPostHomepageRepository topicPostRepository, ITopicPostHomepageQueries topicPostQueries)
        {
            this.topicPostRepository = topicPostRepository;
            this.topicPostQueries = topicPostQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.TopicPost == null || request.TopicPost.Id == 0 || request.TopicPost.PostId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var topicPost = (await topicPostQueries.Gets($"id = {request.TopicPost.Id}")).FirstOrDefault();
            if(topicPost != null)
            {
                topicPost.PostId = request.TopicPost.PostId;
                var rs = await topicPostRepository.Update(topicPost);
                return rs == 0 ? 0 : throw new BusinessException("Common.UpdateFail");
            }
			
            return 0;
        }
    }
}
