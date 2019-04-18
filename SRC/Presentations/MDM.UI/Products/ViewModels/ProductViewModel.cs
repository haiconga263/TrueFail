using MDM.UI.Categories.Models;
using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Products.ViewModels
{
    public class ProductViewModel : Product
    {
        public string Name { set; get; }
        public string Decription { set; get; }

        public int CurrentUoM { set; get; }
        public decimal BuyingCurrentPrice { set; get; }
        public decimal SellingCurrentPrice { set; get; }
        public double CurrentWeight { set; get; }
        public double CurrentCapacity { set; get; }

        public Category Category { set; get; }
        public List<ProductLanguage> Languages { set; get; } = new List<ProductLanguage>();
        public List<ProductPrice> Prices { set; get; } = new List<ProductPrice>();

        public string ImageData { set; get; }
    }
}
