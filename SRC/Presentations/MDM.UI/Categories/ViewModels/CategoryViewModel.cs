using MDM.UI.Categories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Categories.ViewModels
{
    public class CategoryViewModel : Category
    {
        public IEnumerable<CategoryViewModel> Childs { set; get; } = new List<CategoryViewModel>();
    }
}
