using MDM.UI.Vehicles.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Admin.Commands.VehicleCommand;
using Common.Models;
using System.Collections.Generic;
using Common.Helpers;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class VehicleController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IVehicleQueries vehicleQueries = null;
        public VehicleController(IMediator mediator, IVehicleQueries vehicleQueries)
        {
            this.mediator = mediator;
            this.vehicleQueries = vehicleQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await vehicleQueries.Gets()
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser()]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await vehicleQueries.Get(id)
            };
        }

        [HttpGet]
        [Route("types/download")]
        [AuthorizeUser("Administrator")]
        public async Task<FileResult> DownloadTypes()
        {
            var types = await vehicleQueries.GetTypes();

            var excelInputType = new ExcelInputData()
            {
                SheetName = "VehicleType",
                Header = new string[4] {
                    "Mã Loại Phương Tiện",
                    "Tên Loại Phương Tiện",
                    "Thời gian soạn hàng có palleted (phút)",
                    "Thời gian soạn hàng không có palleted (phút)"
                },
                Contents = new List<string[]>()
            };

            foreach (var type in types)
            {
                excelInputType.Contents.Add(new string[4] {
                    type.Code,
                    type.Name,
                    "0", //hardcode
                    "0"  //hardcode
                });
            }

            var excelData = ExcelHelper.Create(new List<ExcelInputData>() { excelInputType }, ExcelExtension.xlsx);

            return File(excelData.Data, excelData.ContentType, $"VehicleType.{excelData.Extension.ToString()}");
        }

        [HttpGet]
        [Route("download")]
        [AuthorizeUser("Administrator")]
        public async Task<FileResult> Downloads()
        {
            var vehicles = await vehicleQueries.Gets();

            var excelInputType = new ExcelInputData()
            {
                SheetName = "Vehicle",
                Header = new string[19] {
                    "Mã Tổ Chức",
                    "Mã Phương Tiện",
                    "Kiểu Phương Tiện",
                    "Biển Số Xe",
                    "Nhiệt Độ",
                    "Số Ngăn",
                    "Sức ChứaThể Tích (m3)",
                    "Sức Chứa Trọng Lượng (kg)",
                    "Thời Gian Bắt Đầu",
                    "Thời Gian Kết Thúc",
                    "Thời Gian Bắt Đầu Nghỉ Trưa",
                    "Thời Gian Kết Thúc Nghỉ Trưa",
                    "Chi Phí Cố Định",
                    "Chi phí trên km",
                    "Chi Phí Thuê Xe",
                    "Độ Thân Thuộc",
                    "Tốc Độ",
                    "Tổng Trọng Lượng",
                    "Lái Xe Mặc Định"
                },
                Contents = new List<string[]>()
            };

            foreach (var vehicle in vehicles)
            {
                excelInputType.Contents.Add(new string[19] {
                    vehicle.OrgCode,
                    vehicle.Code,
                    vehicle.Type.Code,
                    vehicle.Name,
                    vehicle.TemperatureType,
                    vehicle.ZoneCount.ToString(),
                    vehicle.Capacity.ToString(),
                    vehicle.Weight.ToString(),
                    vehicle.StartTime,
                    vehicle.EndTime,
                    vehicle.StartLunchTime,
                    vehicle.EndLunchTime,
                    "0", //hardcode
                    "0", //hardcode
                    "0", //hardcode
                    "0", //hardcode,
                    vehicle.Speed.ToString(),
                    (vehicle.Weight + vehicle.VehicleWeight).ToString(),
                    string.Empty //hardcode
                });
            }

            var excelData = ExcelHelper.Create(new List<ExcelInputData>() { excelInputType }, ExcelExtension.xlsx);

            return File(excelData.Data, excelData.ContentType, $"Vehicle.{excelData.Extension.ToString()}");
        }

        [HttpGet]
        [Route("gets/type")]
        public async Task<APIResult> GetTypes()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await vehicleQueries.GetTypes()
            };
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
