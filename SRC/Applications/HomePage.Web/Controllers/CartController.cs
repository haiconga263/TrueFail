using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using HomePage.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HomePage.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly IOrderHomepageRepository _orderHomepageRepository;
        public CartController(IStringLocalizer<SharedResource> localizer,
            IOrderHomepageRepository orderHomepageRepository) : base(localizer)
        {
            _orderHomepageRepository = orderHomepageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Checkout([Bind] RetailerOrderTemp order)
        {
            if (order != null && !string.IsNullOrEmpty(order.Name))
            {

                var rs = await _orderHomepageRepository.AddAsync(order);
                if (rs > 0)
                {
                    ViewData["alert_msg"] = "Thành công!";
                    ViewData["alert_type"] = "success";
                }

                else
                {
                    ViewData["alert_msg"] = "Thất bại!";
                    ViewData["alert_type"] = "error";
                }
            }
            return View();
        }
    }
}