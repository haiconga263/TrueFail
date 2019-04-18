using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Homepage.Commands.HomeCommand;
using Homepage.Commands.ContactCommand;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.Controls;
using Web.Attributes;
using Homepage.UI.Interfaces;

namespace Homepage.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class ContactHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly IContactHomepageQueries contactHomepageQueries = null;

		public ContactHomepageController(IMediator mediator, IContactHomepageQueries contactHomepageQueries)
		{
			this.mediator = mediator;
			this.contactHomepageQueries = contactHomepageQueries;
		}

		[HttpGet]
		[Route("get/all-contact")]
		[AuthorizeUser()]
		public async Task<APIResult> GetAllFaq()
		{
			var rs = await contactHomepageQueries.GetAllContact();

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}
		[HttpGet]
		[Route("get/contact-detail")]
		[AuthorizeUser()]
		public async Task<APIResult> GetContactById(int contactId)
		{
			var rs = await contactHomepageQueries.GetContactById(contactId);

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}

		[HttpPost]
		[Route("add")]
		[AuthorizeUser()]
		public async Task<APIResult> Insert([FromBody] InsertContactCommand command)
		{
			var id = await mediator.Send(command);
			return new APIResult()
			{
				Result = 0,
				Data = id
			};
		}
		[HttpPost]
		[Route("update")]
		[AuthorizeUser()]
		public async Task<APIResult> Update([FromBody] UpdateContactCommand command)
		{
			var rs = await mediator.Send(command);
			return new APIResult()
			{
				Result = rs
			};
		}
		[HttpPost]
		[Route("delete")]
		[AuthorizeUser()]
		public async Task<APIResult> Delete([FromBody] DeleteContactCommand command)
		{
			var id = await mediator.Send(command);
			return new APIResult()
			{
				Data = new { id = (id > 0) ? id : (int?)null },
				Result = (id > 0) ? 0 : -1,
			};
		}
	}
}
