using GS1.GLN.Commands;
using GS1.UI.GLN.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace GS1.GLN.Controllers
{
	[Route("api/gln")]
	[AuthorizeInUserService]
	public class GLNController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly IGLNQueries glnQueries = null;
		private readonly IGLNRepository glnRepository = null;
		private readonly IAccessTokenQueries accessTokenQueries = null;

		public GLNController(IMediator mediator, IGLNQueries glnQueries, IAccessTokenQueries accessTokenQueries)
		{
			this.mediator = mediator;
			this.glnQueries = glnQueries;
			this.accessTokenQueries = accessTokenQueries;
		}

		[HttpGet]
		[Route("common")]
		public async Task<APIResult> Gets()
		{
			var rs = await glnQueries.GetsAsync();
			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}

		[HttpPost]
		[Route("all")]
		public async Task<APIResult> GetAll()
		{
			var rs = await glnQueries.GetAllAsync();

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}

		[HttpGet]
		[Route("getbyid")]
		public async Task<APIResult> GetById(int id)
		{
			var rs = await glnQueries.GetByIdAsync(id);
			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}

		[HttpPost]
		[Route("insert")]
		public async Task<APIResult> Insert([FromBody]InsertGLNCommand command)
		{
			var id = await mediator.Send(command);
			return new APIResult()
			{
				Data = new { id = (id > 0) ? id : (int?)null },
				Result = (id > 0) ? 0 : -1,
			};
		}

		[HttpPost]
		[Route("update")]
		public async Task<APIResult> Update([FromBody]UpdateGLNCommand command)
		{
			var rs = await mediator.Send(command);
			return new APIResult()
			{
				Result = rs
			};
		}

		[HttpPost]
		[Route("delete")]
		public async Task<APIResult> Delete([FromBody]DeleteGLNCommand command)
		{
			var rs = await mediator.Send(command);
			return new APIResult()
			{
				Result = rs
			};
		}
	}
}


