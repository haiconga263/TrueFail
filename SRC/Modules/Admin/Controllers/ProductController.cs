using Admin.Commands.ProductCommand;
using Common.Helpers;
using Common.Models;
using MDM.UI.Products.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IProductQueries productQueries = null;
        public ProductController(IMediator mediator, IProductQueries productQueries)
        {
            this.mediator = mediator;
            this.productQueries = productQueries;
        }

        [HttpGet]
        [Route("gets/only")]
        [AuthorizeUser()]
        public async Task<APIResult> GetsOnly()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await productQueries.GetsOnly()
            };
        }

        [HttpGet]
        [Route("gets/only/withlang")]
        [AuthorizeUser()]
        public async Task<APIResult> GetsOnlyWithLang()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await productQueries.GetsOnlyWithLang(LanguageId)
            };
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await productQueries.Gets(string.Empty, LanguageId, DateTime.Now)
            };
        }

        [HttpGet]
        [Route("gets/fororder")]
        [AuthorizeUser()]
        public async Task<APIResult> GetsForOrder()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await productQueries.GetsForOrder(string.Empty, LanguageId, DateTime.Now)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser()]
        public async Task<APIResult> Get(int productId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await productQueries.Get(productId)
            };
        }

        [HttpGet]
        [Route("gets/full")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> GetsFull()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await productQueries.GetsFull()
            };
        }

        [HttpGet]
        [Route("get/full")]
        [AuthorizeUser()]
        public async Task<APIResult> GetFull(int productId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await productQueries.GetFull(productId)
            };
        }

        [HttpGet]
        [Route("download")]
        [AuthorizeUser("Administrator")]
        public async Task<FileResult> Download()
        {
            var products = await productQueries.Gets("p.is_used = 1");

            var excelInputType = new ExcelInputData()
            {
                SheetName = "VehicleType",
                Header = new string[17] {
                    "Mã Danh Mục",
                    "Mã Sản Phẩm",
                    "Tên Sản Phẩm",
                    "Giá Thùng",
                    "Giá Lẻ",
                    "Số Lẻ Trên Thùng",
                    "Số Lượng",
                    "Đơn Vị",
                    "Đơn Vị Quy Đổi",
                    "Trọng Lượng (kg)",
                    "Thể Tích (m3)",
                    "Ngày Bắt Đầu",
                    "Ngày Kết Thúc",
                    "Mã Sản Phẩm Cha",
                    "Thuộc Tính Sản Phẩm",
                    "Serial Sản Phẩm",
                    "Nhiệt Độ"
                },
                Contents = new List<string[]>()
            };

            foreach (var product in products)
            {
                excelInputType.Contents.Add(new string[17] {
                    product.Category == null ? string.Empty : product.Category.Code,
                    $"{product.Code}_{product.CurrentUoM}",
                    product.DefaultName,
                    "0", //hardcode
                    "0", //hardcode
                    "0", //hardcode
                    "0", //hardcode
                    product.CurrentUoM.ToString(),
                    string.Empty,
                    product.CurrentWeight.ToString(),
                    product.CurrentCapacity.ToString(),
                    "0", //hardcode
                    "0", //hardcode
                    $"{product.Code}_{product.CurrentUoM}",
                    string.Empty, //hardcode
                    string.Empty, //hardcode
                    string.Empty //hardcode
                });
            }

            var excelData = ExcelHelper.Create(new List<ExcelInputData>() { excelInputType }, ExcelExtension.xlsx);

            return File(excelData.Data, excelData.ContentType, $"Product.{excelData.Extension.ToString()}");
            //return excelData.Data;
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Add([FromBody]AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Update([FromBody]UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
