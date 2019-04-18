using System;
using System.Collections.Generic;
using System.Text;

namespace Homepage.UI
{
	public class AppUrl
	{
		public const string GetProduct = "/api/ProductHomepage/get/product";
		public const string GetProductDetail = "/api/ProductHomepage/get/product-detail?productId=";
		public const string GetProductRelated = "/api/ProductHomepage/get/product-related?productId=";
		public const string GetProductOutstanding = "/api/ProductHomepage/get/product-outstanding";

		public const string GetCategory = "/api/CategoryHomepage/get/category";
		public const string GetCategoryDetail = "/api/CategoryHomepage/get/category-detail?categoryId=";

		public const string GetRetailer = "/api/RetailerHomepage/get/retailer";

		public const string InsertContact = "/api/ContactHomepage/insert";
		public const string InsertOrder = "/api/ProductHomepage/insert";
	}
}
