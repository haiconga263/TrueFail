using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homepage.UI.Interfaces;
using HomePage.Web.Infrastructure;
using MDM.UI.Categories;
using MDM.UI.Categories.Interfaces;
using MDM.UI.Categories.Models;
using MDM.UI.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HomePage.Web.Controllers
{
    public class CommonsController : BaseController
    {
        private readonly ICategoryQueries _categoryQueries;

        public CommonsController(IStringLocalizer<SharedResource> localizer,
            ICategoryQueries categoryQueries) : base(localizer)
        {
            this._categoryQueries = categoryQueries;
        }

        public async Task<IActionResult> LoadCategoryMenu()
        {
            IEnumerable<CategoryViewModel> categories = await _categoryQueries.GetsWithChild(CurrentLanguage);
            return PartialView("_CategoryMenu", categories);
        }
    }
}