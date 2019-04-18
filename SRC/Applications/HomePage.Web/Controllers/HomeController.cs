using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomePage.Web.Models;
using HomePage.Web.Infrastructure;
using Microsoft.Extensions.Localization;
using Homepage.UI.ViewModels;
using Web.Helpers;
using Common;
using Homepage.UI;
using Common.Exceptions;
using System.Net.Http;
using System.Net.Http.Headers;
using Common.Models;


using Homepage.UI.Interfaces;
using HomePage.Web.Common;

namespace HomePage.Web.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IProductHomepageQueries _productHomepageQueries;
		private readonly IContactHomepageRepository _contactHomepageRepository;
		private readonly IRetailerHomepageQueries _retailerHomepageQueries;
		private readonly IPostHomepageQueries _postHomepageQueries;
		private readonly IFaqHomepageQueries _faqHomepageQueries;

		public HomeController(IStringLocalizer<SharedResource> localizer,
			IContactHomepageRepository contact,
			IProductHomepageQueries product,
			 IRetailerHomepageQueries about,
			 IPostHomepageQueries post,
			 IFaqHomepageQueries faq) : base(localizer)
		{
			_contactHomepageRepository = contact;
			_productHomepageQueries = product;
			_retailerHomepageQueries = about;
			_postHomepageQueries = post;
			_faqHomepageQueries = faq;
		}

		public async Task<IActionResult> Index()
		{
			ViewData["HasSlider"] = "true";
			var product = (await _productHomepageQueries.GetProductOutstandingOfHomepage(CurrentLanguage))
				.OrderBy(x => Guid.NewGuid()).Take(4);
			if (product == null)
			{
				return Redirect("/error");
			}

			return View(product);
		}

		public async Task<IActionResult> About()
		{
			var product = await _retailerHomepageQueries.GetRetailerAsync();
			if (product == null)
			{
				return Redirect("/error");
			}

			return View(product);
		}

		public async Task<IActionResult> BuyWhere()
		{
			return View();
		}

		public async Task<IActionResult> CookingRecipe()
		{
			return View();
		}

		public async Task<IActionResult> CookingRecipeDetail()
		{
			return View();
		}

		public async Task<IActionResult> ProductReturn()
		{
			return View();
		}

		public async Task<IActionResult> TheMedia()
		{
			return View();
		}

		public async Task<IActionResult> FarmerNetwork()
		{
			return View();
		}

		public async Task<IActionResult> Gifts()
		{
			return View();
		}

		public async Task<IActionResult> WeWork()
		{
			var allPost = await _postHomepageQueries.GetAllPostByTopicId(14, lang: CurrentLanguage);
			
			return View(allPost);
		}

		public async Task<IActionResult> Contact([Bind] ContactHomepageViewModel contact)
		{
			if (contact != null && !string.IsNullOrEmpty(contact.SenderName))
			{
				contact.CreatedDate = DateTime.Now;
				
				var rs = await _contactHomepageRepository.AddAsync(contact);
				if (rs > 0)
				{
					ViewData["alert_msg"] = "Thành công!";
					ViewData["alert_type"] = "success";
				}
				else
				{
					ViewData["alert_msg"] = "Thất bại!";
					ViewData["alert_type"] = "danger";
				}
			}

			return View();
		}

		public async Task<IActionResult> Faqs()
		{
			var model = await _faqHomepageQueries.GetAllFaq(condition: $"is_used = '1' and l.code ='{CurrentLanguage}' ");

			return View(model);
		}

		public async Task<ActionResult> Blog(int topicId)
		{
			var allPost = await _postHomepageQueries.GetAllPostByTopicId(topicId, lang: CurrentLanguage);

			return View(allPost);
		}

		public ActionResult GetContentById(int blogId)
		{
			var result = new BlogViewModel();
			if (blogId == 1)
			{
				var model = new BlogViewModel()
				{
					Id = 1,
					HagTag = "Đà Lạt",
					Title = "ARITNT ra quân dọn dẹp môi trường tại Đà Lạt",
					DatePost = DateTime.Now,
					Description = "Ngày 30/12/2018 vừa qua Công ty Nước Sốt Đặc Sản Việt DASAVI  đã thực hiện hành trình Đà Lạt xanh lần thứ nhất",
					Content = this._localizer["Blog1"],
					BlogTypeViewModels = new BlogTypeViewModel()
					{
						Id = 1,
						TypeName = "Green spirit"
					},
					Image = "http://dasavi.com.vn/vnt_upload/ours/2018/12/Dl4_n.jpg"
				};
				result = model;
			}
			if (blogId == 2)
			{
				var model = new BlogViewModel()
				{
					Id = 3,
					HagTag = "Đà Lạt",
					Title = "Nông trại Đà Lạt thu nhỏ trong “hộp xanh“",
					DatePost = DateTime.Now,
					Description = "Cải tạo nhà kính rộng hơn 7.000m² sản xuất nông nghiệp công nghệ cao thành khu tham quan miễn phí dành cho du khách, gần nửa năm qua khu vườn  ",
					Content = this._localizer["Blog2"],
					BlogTypeViewModels = new BlogTypeViewModel()
					{
						Id = 2,
						TypeName = "Canh tác xanh"
					},
					Image = "https://dalattrongtoi.com/media/upload/images/tin-tuc/M%C6%B0a%20%C4%90%C3%A0%20L%E1%BA%A1t%20(1).jpg"
				};
				result = model;
			}
			if (blogId == 3)
			{
				var model = new BlogViewModel()
				{
					Id = 5,
					HagTag = "Đà Lạt",
					Title = "Tản mạn cùng Thu Đà Lạt",
					DatePost = DateTime.Now,
					Description = "Đà Lạt vào thu không có lá vàng bay, không có những làn gió heo may lướt qua cùng hương cốm nồng nàn.",
					Content = this._localizer["Blog3"],
					BlogTypeViewModels = new BlogTypeViewModel()
					{
						Id = 3,
						TypeName = "Tản mạn"
					},
					Image = "https://dalattrongtoi.com/media/upload/images/bai-viet/Ma%20R%E1%BB%ABng%20L%E1%BB%AF%20Qu%C3%A1n.jpg"
				};
				result = model;
			}
			if (blogId == 4)
			{
				var model = new BlogViewModel()
				{
					Id = 5,
					HagTag = "Đà Lạt",
					Title = "Tản mạn cùng Thu Đà Lạt",
					DatePost = DateTime.Now,
					Description = "Đà Lạt vào thu không có lá vàng bay, không có những làn gió heo may lướt qua cùng hương cốm nồng nàn.",
					Content = this._localizer["Blog4"],
					BlogTypeViewModels = new BlogTypeViewModel()
					{
						Id = 3,
						TypeName = "Tản mạn"
					},
					Image = "https://dalattrongtoi.com/media/upload/images/bai-viet/Ma%20R%E1%BB%ABng%20L%E1%BB%AF%20Qu%C3%A1n.jpg"
				};
				result = model;
			}

			return PartialView(result);
		}

		public async Task<ActionResult> GetBlog(string name, int blogId)
		{

			var post = await _postHomepageQueries.GetPostById(blogId, lang: CurrentLanguage);
			
			return View(post);
		}

		public ActionResult Trace()
		{
			return View();
		}

		public ActionResult Value()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult> SearchAsync(string searchString)
		{
			var result = new List<ProductHomepageViewModel>();
			if (String.IsNullOrEmpty(searchString))
			{
				return RedirectToAction("Index","Home");
			}
			else
			{
				var search = CommonHelper.SafePlainText(searchString);
				var products = await _productHomepageQueries.GetProductAsync(lang: CurrentLanguage);
				if (products == null)
				{
					return Redirect("/error");
				}
				 result = products.Where(x => x.Name.ToUpper().Contains(search.ToUpper())).ToList();
				if (result.Count() == 0)
				{
					ViewBag.Notification = "Không tìm thấy kết quả.";
				}
			}

			
			return View(result);
		}

	}
}
