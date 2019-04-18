using Homepage.Commands.TopicCommand;
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
	public class TopicHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly ITopicHomepageQueries topicHomepageQueries = null;
		public TopicHomepageController(IMediator mediator, ITopicHomepageQueries topicHomepageQueries )
		{
			this.mediator = mediator;
			this.topicHomepageQueries = topicHomepageQueries;
		}

		[HttpGet]
		[Route("get/all-topic")]
		[AuthorizeUser()]
		public async Task<APIResult> GetAllFaq()
		{
			var rs = await topicHomepageQueries.GetAllTopic();

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}
		[HttpGet]
		[Route("get/topic-detail")]
		[AuthorizeUser()]
		public async Task<APIResult> GetFaqById(int topicId)
		{
			var rs = await topicHomepageQueries.GetTopicById(topicId);

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
