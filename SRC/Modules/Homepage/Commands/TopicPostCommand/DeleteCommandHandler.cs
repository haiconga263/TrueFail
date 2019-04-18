using Homepage.UI.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.TopicPostCommand
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
		private readonly ITopicPostHomepageRepository topicPostRepository = null;
		public DeleteCommandHandler(ITopicPostHomepageRepository topicPostRepository)
        {
            this.topicPostRepository = topicPostRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await topicPostRepository.Delete(request.PostId);
        }
    }
}
