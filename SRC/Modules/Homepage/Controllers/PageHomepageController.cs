using Homepage.Commands.PageCommand;
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
	public class PageHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly IPageHomepageQueries pageHomepageQueries = null;
		public PageHomepageController(IMediator mediator, IPageHomepageQueries pageHomepageQueries)
		{
			this.mediator = mediator;
			this.pageHomepageQueries = pageHomepageQueries;
		}

		[HttpGet]
		[Route("get/all-page")]
		[AuthorizeUser()]
		public async Task<APIResult> GetAllPage()
		{
			var rs = await pageHomepageQueries.GetAllPage();

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}
		[HttpGet]
		[Route("get/page-detail")]
		[AuthorizeUser()]
		public async Task<APIResult> GetPageById(int pageId)
		{
			var rs = await pageHomepageQueries.GetPageByIdAsync(pageId);

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
