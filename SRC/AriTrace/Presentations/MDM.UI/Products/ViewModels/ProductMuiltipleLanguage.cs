using MDM.UI.ProductLanguages.Models;
using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Products.ViewModels
{
    public class ProductMuiltipleLanguage : Product
    {
        public List<ProductLanguage> ProductLanguages { get; set; }
        public string ImageData { get; set; }
        public bool IsChangedImage { get; set; }
    }
}
