using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;
using Homepage.UI.Interfaces;

namespace Homepage.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class RetailerHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly IRetailerHomepageQueries retailerHomepageQueries = null;

		public RetailerHomepageController(IMediator mediator, IRetailerHomepageQueries retailerHomepageQueries)
		{
			this.mediator = mediator;
			this.retailerHomepageQueries = retailerHomepageQueries;
		}
		//[HttpGet]
		//[Route("get/retailer")]
		//public async Task<APIResult> GetRetailerOfHomepage()
		//{
		//	var rs = await retailerHomepageQueries.GetRetailerAsync();
		//	return new APIResult()
		//	{
		//		Result = 0,
		//		Data = rs
		//	};
		//}
	}
}
