using Homepage.UI.Models;
using MDM.UI.Distributions.Models;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Homepage.Commands.TopicPostCommand
{
    public class UpdateCommand : BaseCommand<int>
    {
        public TopicPost TopicPost { set; get; }
        public UpdateCommand(TopicPost post)
        {
            this.TopicPost = post;
        }
    }
}
