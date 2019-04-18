using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Homepage.UI;
using Homepage.UI.Interfaces;
using Homepage.UI.ViewModels;
using HomePage.Web.Infrastructure;
using MDM.UI.Categories;
using MDM.UI.Categories.Interfaces;
using MDM.UI.Categories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Helpers;

namespace HomePage.Web.Controllers
{
	public class ProductsController : BaseController
	{
		private const int PAGE_SIZE_DEFAULT = 12;
		private readonly IProductHomepageQueries _productHomepageQueries;
		private readonly ICategoryQueries _categoryQueries;

		public ProductsController(IStringLocalizer<SharedResource> localize,
			IProductHomepageQueries productHomepageQueries, ICategoryQueries categoryQueries) : base(localize)
		{
			_productHomepageQueries = productHomepageQueries;
			_categoryQueries = categoryQueries;
		}

		public async Task<IActionResult> Index(string cateId, int page = 1, int pageSize = PAGE_SIZE_DEFAULT, string sortby = "")
		{

			if (page < 1)
				page = 1;
			ViewData["categories"] = await _categoryQueries.Gets(CurrentLanguage);
			IEnumerable<ProductHomepageViewModel> products = null;
			if (string.IsNullOrWhiteSpace(cateId))
			{
				ViewData["CateID"] = "";
				products = await _productHomepageQueries.GetProductAsync(lang: CurrentLanguage);
				if (products == null)
				{
					return Redirect("/error");
				}
			}
			else
			{
				ViewData["CateID"] = cateId;
				IEnumerable<Category> categories = await _categoryQueries.Gets(CurrentLanguage);

				products = await GetProductByCategoryAsync(int.Parse(cateId), categories);

				if (products == null)
				{
					return Redirect("/error");
				}

			}

			ViewData["TotalProduct"] = products.Count();
			ViewData["TotalPage"] = (int)Math.Ceiling(decimal.Divide(products.Count(), pageSize));
			ViewData["currentPage"] = page;
			ViewData["pagesize"] = pageSize;
			ViewData["sortby"] = sortby;

			switch (sortby)
			{
				case "price-low-to-high":
					products = products.OrderBy(x => x.SellingCurrentPrice);
					break;
				case "price-high-to-low":
					products = products.OrderByDescending(x => x.SellingCurrentPrice);
					break;
				case "product-name":
				default:
					products = products.OrderBy(x => x.Name).ToList();
					break;
			}
			products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			return View(products);
		}

		private async Task<IEnumerable<ProductHomepageViewModel>> GetProductByCategoryAsync(int cateId, IEnumerable<Category> categories)
		{
			IEnumerable<ProductHomepageViewModel> products = new List<ProductHomepageViewModel>();
			products = products.Concat((await _productHomepageQueries.GetProductByCategory(cateId, CurrentLanguage)) ?? new List<ProductHomepageViewModel>());
			if (categories != null && categories.Count() > 0)
			{
				foreach (var category in categories.Where(x => x.ParentId == cateId))
				{
					products = products.Concat((await GetProductByCategoryAsync(category.Id, categories)) ?? new List<ProductHomepageViewModel>());
				}
			}
			return products;
		}


		public async Task<IActionResult> Detail(string name, string id)
		{
			var product = await _productHomepageQueries.GetProductDetailOfHomepageAsync(int.Parse(id), CurrentLanguage);
			if (product == null)
			{
				return Redirect("/error");
			}

			ViewData["Related"] = (await _productHomepageQueries.GetProductRelatedAsync(product.CategoryId ?? 0, CurrentLanguage))
				.Where(x => x.Id != (product.Id))?
				.OrderBy(x => Guid.NewGuid()).Take(4) ?? new List<ProductHomepageViewModel>();

			return View(product);
		}
	}
}