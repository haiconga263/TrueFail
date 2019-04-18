using MDM.UI.Categories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Categories.ViewModels
{
    public class CategoryLanguageViewModel : Category
    {
            public List<CategoryLanguage> Languages { set; get; } = new List<CategoryLanguage>();
    }
}
