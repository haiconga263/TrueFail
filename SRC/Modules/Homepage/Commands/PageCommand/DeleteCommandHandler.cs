using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.PageCommand
{
	public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
	{
		private readonly IPageHomepageRepository pageRepository = null;
		public DeleteCommandHandler(IPageHomepageRepository pageRepository)
		{
			this.pageRepository = pageRepository;
		}
		public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
		{
			return await pageRepository.DeleteAsync(DeleteBuild(new Page()
			{
				Id = request.PageId
			}, request.LoginSession));
		}
	}
}
