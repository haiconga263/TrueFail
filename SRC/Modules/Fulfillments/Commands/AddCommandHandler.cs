using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Fulfillments.Interfaces;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Fulfillments.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IFulfillmentRepository fulfillmentRepository = null;
        private readonly IFulfillmentQueries fulfillmentQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public AddCommandHandler(IFulfillmentRepository fulfillmentRepository, IFulfillmentQueries fulfillmentQueries, ILocationRepository locationRepository)
        {
            this.fulfillmentRepository = fulfillmentRepository;
            this.fulfillmentQueries = fulfillmentQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.Fulfillment == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

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

                        request.Fulfillment = CreateBuild(request.Fulfillment, request.LoginSession);
                        request.Fulfillment.AddressId = null;
                        request.Fulfillment.ContactId = null;
                        request.Fulfillment.Code = await fulfillmentQueries.GenarateCode();
                        request.Fulfillment.Id = await fulfillmentRepository.Add(request.Fulfillment);

                        request.Fulfillment.Address.Id = 0;
                        request.Fulfillment.Address.ObjectType = LocationOjectType.F.ToString();
                        request.Fulfillment.Address.ObjectId = request.Fulfillment.Id;
                        request.Fulfillment.Address.IsUsed = true;
                        request.Fulfillment.Address = CreateBuild(request.Fulfillment.Address, request.LoginSession);
                        var addressId = await locationRepository.AddOrUpdateAddress(request.Fulfillment.Address);

                        request.Fulfillment.Contact.Id = 0;
                        request.Fulfillment.Contact.ObjectType = LocationOjectType.F.ToString();
                        request.Fulfillment.Contact.ObjectId = request.Fulfillment.Id;
                        request.Fulfillment.Contact.IsUsed = true;
                        request.Fulfillment.Contact = CreateBuild(request.Fulfillment.Contact, request.LoginSession);
                        var contactId = await locationRepository.AddOrUpdateContact(request.Fulfillment.Contact);

                        request.Fulfillment.AddressId = addressId;
                        request.Fulfillment.ContactId = contactId;


                        rs = await fulfillmentRepository.Update(request.Fulfillment);
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
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
