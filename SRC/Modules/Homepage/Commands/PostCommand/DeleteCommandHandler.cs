using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.PostCommand
{
	public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
	{
		private readonly IPostHomepageRepository postRepository = null;
		public DeleteCommandHandler(IPostHomepageRepository postRepository)
		{
			this.postRepository = postRepository;
		}
		public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
		{
			return await postRepository.DeleteAsync(DeleteBuild(new Post()
			{
				Id = request.PostId
			}, request.LoginSession));
		}
	}
}
