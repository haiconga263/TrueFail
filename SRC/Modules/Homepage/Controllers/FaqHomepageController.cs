using Homepage.Commands.FaqCommand;
using Homepage.UI.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Homepage.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class FaqHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly IFaqHomepageQueries faqHomepageQueries = null;
		public FaqHomepageController(IMediator mediator, IFaqHomepageQueries faqHomepageQueries )
		{
			this.mediator = mediator;
			this.faqHomepageQueries = faqHomepageQueries;
		}

		[HttpGet]
		[Route("get/all-faq")]
		[AuthorizeUser()]
		public async Task<APIResult> GetAllFaq()
		{
			var rs = await faqHomepageQueries.GetAllFaq();

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}
		[HttpGet]
		[Route("get/faq-detail")]
		[AuthorizeUser()]
		public async Task<APIResult> GetFaqById(int faqId)
		{
			var rs = await faqHomepageQueries.GetFaqByIdAsync(faqId);

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}

		[HttpPost]
		[Route("add")]
		[AuthorizeUser()]
		public async Task<APIResult> Insert([FromBody] AddCommand command)
		{
			var rs = await mediator.Send(command);
			return new APIResult()
			{
				Result = rs
			};
		}
		[HttpPost]
		[Route("update")]
		[AuthorizeUser()]
		public async Task<APIResult> Update([FromBody] UpdateCommand command)
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
		public async Task<APIResult> Delete([FromBody] DeleteCommand command)
		{
			var rs = await mediator.Send(command);
			return new APIResult()
			{
				Result = rs
			};
		}


	}
}
