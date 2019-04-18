using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Vehicles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.VehicleCommand
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IVehicleRepository vehicleRepository = null;
        private readonly IVehicleQueries vehicleQueries = null;
        public AddCommandHandler(IVehicleRepository vehicleRepository, IVehicleQueries vehicleQueries)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleQueries = vehicleQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if(request.Vehicle == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if (request.Vehicle.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }
            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Vehicle.ImageData?.Length > 200)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Vehicle.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Vehicle.ImageData.Substring(request.Vehicle.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Vehicle.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.VehicleImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            request.Vehicle.Code = await vehicleQueries.GenarateCode();
            request.Vehicle = CreateBuild(request.Vehicle, request.LoginSession);
            var rs = await vehicleRepository.Add(request.Vehicle);
            if(rs != 0)
            {
                CommonHelper.DeleteImage(request.Vehicle.ImageURL);
            }

            return rs == 0 ? -1 : 0;
        }
    }
}
