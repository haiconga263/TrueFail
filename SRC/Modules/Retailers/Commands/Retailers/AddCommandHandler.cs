using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Retailers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Retailers.Commands.Commands.Retailers
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IRetailerRepository retailerRepository = null;
        private readonly ILocationRepository locationRepository = null;
        public AddCommandHandler(IRetailerRepository retailerRepository, ILocationRepository locationRepository = null)
        {
            this.retailerRepository = retailerRepository;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Retailer == null || request.Retailer.Address == null || request.Retailer.Contact == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if (request.Retailer.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }
            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Retailer.ImageData?.Length > 200)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Retailer.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Retailer.ImageData.Substring(request.Retailer.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Retailer.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.RetailerImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            var rs = -1;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        retailerRepository.JoinTransaction(conn, trans);
                        locationRepository.JoinTransaction(conn, trans);


                        request.Retailer = CreateBuild(request.Retailer, request.LoginSession);
                        if(!request.Retailer.IsCompany)
                        {
                            request.Retailer.TaxCode = string.Empty;
                        }
                        request.Retailer.Id = await retailerRepository.Add(request.Retailer);

                        request.Retailer.Address.Id = 0;
                        request.Retailer.Address.ObjectType = LocationOjectType.R.ToString();
                        request.Retailer.Address.ObjectId = request.Retailer.Id;
                        request.Retailer.Address.IsUsed = true;
                        request.Retailer.Address = CreateBuild(request.Retailer.Address, request.LoginSession);
                        var addressId = await locationRepository.AddOrUpdateAddress(request.Retailer.Address);

                        request.Retailer.Contact.Id = 0;
                        request.Retailer.Contact.ObjectType = LocationOjectType.R.ToString();
                        request.Retailer.Contact.ObjectId = request.Retailer.Id;
                        request.Retailer.Contact.IsUsed = true;
                        request.Retailer.Contact = CreateBuild(request.Retailer.Contact, request.LoginSession);
                        var contactId = await locationRepository.AddOrUpdateContact(request.Retailer.Contact);

                        request.Retailer.AddressId = addressId;
                        request.Retailer.ContactId = contactId;


                        rs = await retailerRepository.Update(request.Retailer);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.GetLogger().Error(ex.Message);
                        return rs = -1;
                    }
                    finally
                    {
                        if(rs == 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Commit();
                            }
                            catch { }
                            CommonHelper.DeleteImage(request.Retailer.ImageURL);
                        }
                    }
                }
            };

            return rs;
        }
    }
}
