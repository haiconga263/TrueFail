using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Fulfillments.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Fulfillments.Commands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IFulfillmentRepository fulfillmentRepository = null;
        private readonly IFulfillmentQueries fulfillmentQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public UpdateCommandHandler(IFulfillmentRepository fulfillmentRepository, IFulfillmentQueries fulfillmentQueries, ILocationRepository locationRepository)
        {
            this.fulfillmentRepository = fulfillmentRepository;
            this.fulfillmentQueries = fulfillmentQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Fulfillment == null || request.Fulfillment.Id == 0 || request.Fulfillment.Address == null || request.Fulfillment.Contact == null)
            {
                throw new BusinessException("Fulfillment.NotExisted");
            }

            var fulfillment = await fulfillmentQueries.Get(request.Fulfillment.Id);
            if (fulfillment == null)
            {
                throw new BusinessException("Fulfillment.NotExisted");
            }

            string oldImageUrl = request.Fulfillment.ImageURL;

            if (request.Fulfillment.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }
            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Fulfillment.ImageData?.Length > 100)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Fulfillment.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Fulfillment.ImageData.Substring(request.Fulfillment.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Fulfillment.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.FulfillmentImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            var rs = -1;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        fulfillmentRepository.JoinTransaction(conn, trans);
                        locationRepository.JoinTransaction(conn, trans);

                        fulfillment.Address = fulfillment.Address ?? new Address();
                        fulfillment.Address.Street = request.Fulfillment.Address.Street;
                        fulfillment.Address.CountryId = request.Fulfillment.Address.CountryId;
                        fulfillment.Address.ProvinceId = request.Fulfillment.Address.ProvinceId;
                        fulfillment.Address.DistrictId = request.Fulfillment.Address.DistrictId;
                        fulfillment.Address.WardId = request.Fulfillment.Address.WardId;
                        fulfillment.Address.Longitude = request.Fulfillment.Address.Longitude;
                        fulfillment.Address.Latitude = request.Fulfillment.Address.Latitude;
                        fulfillment.Address = UpdateBuild(fulfillment.Address, request.LoginSession);
                        if (await locationRepository.AddOrUpdateAddress(fulfillment.Address) == -1)
                        {
                            return rs = -1;
                        }

                        fulfillment.Contact = fulfillment.Contact ?? new Contact();
                        fulfillment.Contact.Name = request.Fulfillment.Contact.Name;
                        fulfillment.Contact.Phone = request.Fulfillment.Contact.Phone;
                        fulfillment.Contact.Email = request.Fulfillment.Contact.Email;
                        fulfillment.Contact.Gender = request.Fulfillment.Contact.Gender;
                        fulfillment.Contact = UpdateBuild(fulfillment.Contact, request.LoginSession);
                        if (await locationRepository.AddOrUpdateContact(fulfillment.Contact) == -1)
                        {
                            return rs = -1;
                        }

                        fulfillment = UpdateBuild(fulfillment, request.LoginSession);
                        fulfillment.ImageURL = request.Fulfillment.ImageURL;
                        fulfillment.Name = request.Fulfillment.Name;
                        fulfillment.Code = request.Fulfillment.Code;
                        fulfillment.ManagerId = request.Fulfillment.ManagerId;
                        fulfillment.IsUsed = request.Fulfillment.IsUsed;
                        rs = await fulfillmentRepository.Update(fulfillment);
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                            CommonHelper.DeleteImage(oldImageUrl);
                        }
                        else
                        {
                            try
                            {
                                trans.Commit();
                            }
                            catch { }
                            CommonHelper.DeleteImage(request.Fulfillment.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
