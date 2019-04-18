using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.FaqCommand
{
	public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
	{
		private readonly IFaqHomepageRepository faqRepository = null;
		public DeleteCommandHandler(IFaqHomepageRepository faqRepository)
		{
			this.faqRepository = faqRepository;
		}
		public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
		{
			return await faqRepository.DeleteAsync(DeleteBuild(new Faq()
			{
				Id = request.FaqId
			}, request.LoginSession));
		}
	}
}
