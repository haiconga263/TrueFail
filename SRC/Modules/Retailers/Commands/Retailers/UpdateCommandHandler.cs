using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
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
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IRetailerRepository retailerRepository = null;
        private readonly IRetailerQueries retailerQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public UpdateCommandHandler(IRetailerRepository retailerRepository, IRetailerQueries retailerQueries, ILocationRepository locationRepository)
        {
            this.retailerRepository = retailerRepository;
            this.retailerQueries = retailerQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Retailer == null || request.Retailer.Id == 0 || request.Retailer.Address == null || request.Retailer.Contact == null)
            {
                throw new BusinessException("Retailer.NotExisted");
            }

            var retailer = await retailerQueries.Get(request.Retailer.Id);
            if(retailer == null)
            {
                throw new BusinessException("Retailer.NotExisted");
            }

            string oldImageUrl = request.Retailer.ImageURL;

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

                        retailer.Address = retailer.Address == null ? new Address() : retailer.Address;
                        retailer.Address.Street = request.Retailer.Address.Street;
                        retailer.Address.CountryId = request.Retailer.Address.CountryId;
                        retailer.Address.ProvinceId = request.Retailer.Address.ProvinceId;
                        retailer.Address.DistrictId = request.Retailer.Address.DistrictId;
                        retailer.Address.WardId = request.Retailer.Address.WardId;
                        retailer.Address.Longitude = request.Retailer.Address.Longitude;
                        retailer.Address.Latitude = request.Retailer.Address.Latitude;
                        retailer.Address = UpdateBuild(retailer.Address, request.LoginSession);
                        if(await locationRepository.AddOrUpdateAddress(retailer.Address) == -1)
                        {
                            return rs = -1;
                        }

                        retailer.Contact = retailer.Contact == null ? new Contact() : retailer.Contact;
                        retailer.Contact.Name = request.Retailer.Contact.Name;
                        retailer.Contact.Phone = request.Retailer.Contact.Phone;
                        retailer.Contact.Email = request.Retailer.Contact.Email;
                        retailer.Contact.Gender = request.Retailer.Contact.Gender;
                        retailer.Contact = UpdateBuild(retailer.Contact, request.LoginSession);
                        if (await locationRepository.AddOrUpdateContact(retailer.Contact) == -1)
                        {
                            return rs = -1;
                        }

                        retailer = UpdateBuild(retailer, request.LoginSession);
                        retailer.ImageURL = request.Retailer.ImageURL;
                        retailer.Name = request.Retailer.Name;
                        retailer.UserId = request.Retailer.UserId;
                        retailer.IsUsed = request.Retailer.IsUsed;
                        retailer.IsCompany = request.Retailer.IsCompany;
                        if (!request.Retailer.IsCompany)
                        {
                            retailer.TaxCode = string.Empty;
                        }
                        else
                        {
                            retailer.TaxCode = request.Retailer.TaxCode;
                        }
                        rs = await retailerRepository.Update(retailer);
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                            if (request.Retailer.ImageData?.Length > 200)
                                CommonHelper.DeleteImage(oldImageUrl);
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
            }

            return rs;
        }
    }
}
