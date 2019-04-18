using Homepage.UI.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homepage.Commands.HomeCommand;
using Homepage.Constants;
using Homepage.UI.ViewModels;
using MongoDB.Bson;
using Web.Controllers;
using Web.Controls;

namespace Homepage.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class ProductHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly IProductHomepageQueries productHomepageQueries = null;
		//private readonly IProductHomepageRepository productHomepageRepository = null;

		public ProductHomepageController(IMediator mediator, IProductHomepageQueries productHomepageQueries)
		{
			this.mediator = mediator;
			this.productHomepageQueries = productHomepageQueries;
			//this.productHomepageRepository = productHomepageRepository;
		}
		//[HttpGet]
		//[Route("get/product")]
		//public async Task<APIResult> GetProductOfHomepage(int? p)
		//{
		//	var pageIndex = p ?? 1;
		//	var rs = await productHomepageQueries.GetProductAsync(string.Empty,pageIndex, SiteConstants.ProductHomepageListPageSize);
		//	return new APIResult()
		//	{
		//		Result = 0,
		//		Data = rs
		//	};
		//}

		//[HttpGet]
		//[Route("get/product-detail")]
		//public async Task<APIResult> GetProductDetailOfHomepage(int productId)
		//{
		//	var rs = await productHomepageQueries.GetProductDetailOfHomepageAsync(productId);

		//	return new APIResult()
		//	{
		//		Result = 0,
		//		Data = rs
		//	};
		//}
		//[HttpGet]
		//[Route("get/product-related")]
		//public async Task<APIResult> GetProductRelatedOfHomepage(int productId)
		//{
		//	var product = await productHomepageQueries.GetProductDetailOfHomepageAsync(productId);

		//	var rs = await productHomepageQueries.GetProductRelatedAsync(product.Category.Id);

		//	return new APIResult()
		//	{
		//		Result = 0,
		//		Data = rs
		//	};
		//}
		//[HttpPost]
		//[Route("insert")]
		//public async Task<APIResult> Insert([FromBody] InsertOrderCommand command)
		//{
		//	var id = await mediator.Send(command);
		//	return new APIResult()
		//	{
		//		Data = new { id = (id > 0) ? id : (int?)null },
		//		Result = (id > 0) ? 0 : -1,
		//	};
		//}
		//[HttpGet]
		//[Route("get/product-outstanding")]
		//public async Task<APIResult> GetProductOutstandingOfHomepage()
		//{
		//	//var product = await productHomepageQueries.GetProductOutstandingOfHomepage();

		//	var rs = await productHomepageQueries.GetProductOutstandingOfHomepage();

		//	return new APIResult()
		//	{
		//		Result = 0,
		//		Data = rs
		//	};
		//}
	}
}
