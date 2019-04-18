using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MDM.UI.Categories.Models;

namespace Homepage.UI.ViewModels
{
    public class ProductHomepageViewModel : Product
    {
        public string Name { set; get; }
        public string Decription { set; get; }
        public int CurrentUoM { set; get; }
        public decimal SellingCurrentPrice { set; get; }
        public List<ProductPrice> Prices { set; get; } = new List<ProductPrice>();
        public Category Category { get; set; } = new Category();
	}

}
