using Homepage.UI.Models;
using MDM.UI.Distributions.Models;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Homepage.Commands.TopicPostCommand
{
    public class AddCommand : BaseCommand<int>
    {
        public TopicPost TopicPost { set; get; }
        public AddCommand(TopicPost post)
        {
			this.TopicPost = post;
        }
    }
}
