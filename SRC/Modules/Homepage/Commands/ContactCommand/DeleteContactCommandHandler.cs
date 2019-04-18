using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.ContactCommand
{
	public class DeleteContactCommandHandler : BaseCommandHandler<DeleteContactCommand, int>
	{
		private readonly IContactHomepageRepository contactRepository = null;
		public DeleteContactCommandHandler(IContactHomepageRepository contactRepository)
		{
			this.contactRepository = contactRepository;
		}
		public override async Task<int> HandleCommand(DeleteContactCommand request, CancellationToken cancellationToken)
		{
			return await contactRepository.DeleteAsync(DeleteBuild(new Contact()
			{
				Id = request.ContactId
			}, request.LoginSession));
		}
	}
}
