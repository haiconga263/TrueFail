using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.UI.Interfaces;
using Order.UI.ViewModels;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Order.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class RetailerOrderHomepageController : BaseController
	{
		private readonly IRetailerOrderHomepageQueries _retailerOrderHomepageQueries = null;
		public RetailerOrderHomepageController(IRetailerOrderHomepageQueries retailerOrderHomepageQueries)
		{
			this._retailerOrderHomepageQueries = retailerOrderHomepageQueries;
		}

		[HttpGet]
		[Route("gets/retailer-ordertemp")]
		[AuthorizeUser("Administrator")]
		public async Task<APIResult> GetsRetailerOrderTemp()
		{
			var rs = await _retailerOrderHomepageQueries.Gets();
			var result = new List<RetailerOrderHomepageViewModel>();
			foreach (var item in rs)
			{
				var vm = CommonHelper.Mapper<Order.UI.Models.RetailerOrderHomepage, RetailerOrderHomepageViewModel>(item);
				vm.RetailerOrderHomepageDetailViewModels = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RetailerOrderHomepageDetailViewModel>>(vm.OrderDetail);
				
				result.Add(vm);
			}
			return new APIResult()
			{
				Result = 0,
				Data = result
			};
		}
		[HttpGet]
		[Route("get/retailer-ordertemp-byid")]
		[AuthorizeUser("Administrator")]
		public async Task<APIResult> GetsRetailerOrderTempById()
		{
			var rs = await _retailerOrderHomepageQueries.Gets();
			var result = new List<RetailerOrderHomepageViewModel>();
			foreach (var item in rs)
			{
				var vm = CommonHelper.Mapper<Order.UI.Models.RetailerOrderHomepage, RetailerOrderHomepageViewModel>(item);
				var detail = Newtonsoft.Json.JsonConvert.DeserializeObject<RetailerOrderHomepageDetailViewModel>(vm.OrderDetail);
				vm.RetailerOrderHomepageDetailViewModels.Add(detail);
				result.Add(vm);
			}
			return new APIResult()
			{
				Result = 0,
				Data = result
			};
		}
	}
}
