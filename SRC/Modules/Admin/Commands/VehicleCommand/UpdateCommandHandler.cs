using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Vehicles.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.VehicleCommand
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IVehicleRepository vehicleRepository = null;
        private readonly IVehicleQueries vehicleQueries = null;
        public UpdateCommandHandler(IVehicleRepository vehicleRepository, IVehicleQueries vehicleQueries)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleQueries = vehicleQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Vehicle == null || request.Vehicle.Id == 0)
            {
                throw new BusinessException("Vehicle.NotExisted");
            }

            var vehicle = await vehicleQueries.Get(request.Vehicle.Id);
            if(vehicle == null)
            {
                throw new BusinessException("Vehicle.NotExisted");
            }
            if (request.Vehicle.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }

            string oldImageUrl = request.Vehicle.ImageURL;

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

            request.Vehicle.Code = vehicle.Code;
            request.Vehicle.CreatedDate = vehicle.CreatedDate;
            request.Vehicle.CreatedBy = vehicle.CreatedBy;
            request.Vehicle = UpdateBuild(request.Vehicle, request.LoginSession);
            var rs = await vehicleRepository.Update(request.Vehicle);

            if(rs == 0)
            {
                if (request.Vehicle.ImageData?.Length > 200)
                {
                    LogHelper.GetLogger().Debug($"ImageData Length: {request.Vehicle.ImageData.Length}. Deleted Image: {oldImageUrl}");
                    CommonHelper.DeleteImage(oldImageUrl);
                }
            }
            else
            {
                LogHelper.GetLogger().Debug($"Delete Image for process failed. Deleted Image: {request.Vehicle.ImageURL}");
                CommonHelper.DeleteImage(request.Vehicle.ImageURL);
            }

            return rs;
        }
    }
}
