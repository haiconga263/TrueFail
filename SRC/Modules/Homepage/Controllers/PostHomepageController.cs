using Homepage.Commands.PostCommand;
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
	public class PostHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly IPostHomepageQueries postHomepageQueries = null;
		public PostHomepageController(IMediator mediator, IPostHomepageQueries postHomepageQueries)
		{
			this.mediator = mediator;
			this.postHomepageQueries = postHomepageQueries;
		}

		[HttpGet]
		[Route("get/all-post")]
		[AuthorizeUser()]
		public async Task<APIResult> GetAllPost()
		{
			var rs = await postHomepageQueries.GetAllPost();

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}
		[HttpGet]
		[Route("get/post-detail")]
		[AuthorizeUser()]
		public async Task<APIResult> GetPostById(int postId)
		{
			var rs = await postHomepageQueries.GetPost(postId);

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
