using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Retailers.UI;
using Web.Helpers;
using MDM.UI.Distributions.ViewModels;

namespace Retailers.Commands.Commands.Retailers
{
    public class UpdateLocationCommandHandler : BaseCommandHandler<UpdateLocationCommand, int>
    {
        private readonly IRetailerRepository retailerRepository = null;
        private readonly IRetailerQueries retailerQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public UpdateLocationCommandHandler(IRetailerRepository retailerRepository, IRetailerQueries retailerQueries, ILocationRepository locationRepository)
        {
            this.retailerRepository = retailerRepository;
            this.retailerQueries = retailerQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            if (request.Location == null || request.Location.RetailerId == 0 || request.Location.Address == null || request.Location.Contact == null)
            {
                throw new BusinessException("RetailerLocation.NotExisted");
            }

            var retailerId = -1;
            if (request.LoginSession.Roles.FirstOrDefault(r => r == "Administrator") == null)
            {
                var retailer = await retailerQueries.Get(request.Location.RetailerId);
                if (retailer == null)
                {
                    throw new BusinessException("Retailer.NotExisted");
                }
                retailerId = retailer.Id;
            }

            var location = await retailerQueries.GetRetailerLocation(request.Location.Id);
            if (location == null)
            {
                throw new BusinessException("RetailerLocation.NotExisted");
            }

            string oldImageUrl = request.Location.ImageURL;

            if (request.Location.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }
            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Location.ImageData?.Length > 200)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Location.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Location.ImageData.Substring(request.Location.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Location.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.RetailerImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            var distributions = await WebHelper.HttpGet<IEnumerable<DistributionViewModel>>(GlobalConfiguration.APIGateWayURI, AppUrl.GetDistributions, request.LoginSession.AccessToken);
            double minDistance = double.MaxValue;
            bool isInDisRadius = false;
            DistributionViewModel chooseDis = null;
            foreach(var dis in distributions)
            {
                var distance = 1000 * CommonHelper.DistanceBetween2Points(dis.Address.Latitude, dis.Address.Longitude, request.Location.Address.Latitude, request.Location.Address.Longitude);
                if(isInDisRadius)
                {
                    if(distance < minDistance && distance<= dis.Radius)
                    {
                        minDistance = distance;
                        chooseDis = dis;
                    }
                }
                else
                {
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        chooseDis = dis;
                    }
                    if(distance <= dis.Radius)
                    {
                        isInDisRadius = true;
                    }
                }
            }
            if(chooseDis != null)
            {
                request.Location.DistributionId = chooseDis.Id;
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

                        location.Address = location.Address == null ? new Address() : location.Address;
                        location.Address.Street = request.Location.Address.Street;
                        location.Address.CountryId = request.Location.Address.CountryId;
                        location.Address.ProvinceId = request.Location.Address.ProvinceId;
                        location.Address.DistrictId = request.Location.Address.DistrictId;
                        location.Address.WardId = request.Location.Address.WardId;
                        location.Address.Longitude = request.Location.Address.Longitude;
                        location.Address.Latitude = request.Location.Address.Latitude;
                        location.Address = UpdateBuild(location.Address, request.LoginSession);
                        if (await locationRepository.AddOrUpdateAddress(location.Address) == -1)
                        {
                            return rs = -1;
                        }

                        location.Contact = location.Contact == null ? new Contact() : location.Contact;
                        location.Contact.Name = request.Location.Contact.Name;
                        location.Contact.Phone = request.Location.Contact.Phone;
                        location.Contact.Email = request.Location.Contact.Email;
                        location.Contact.Gender = request.Location.Contact.Gender;
                        location.Contact = UpdateBuild(location.Contact, request.LoginSession);
                        if (await locationRepository.AddOrUpdateContact(location.Contact) == -1)
                        {
                            return rs = -1;
                        }

                        location = UpdateBuild(location, request.LoginSession);
                        location.ImageURL = request.Location.ImageURL;
                        location.Name = request.Location.Name;
                        location.Description = request.Location.Description;
                        location.DistributionId = request.Location.DistributionId;
                        location.IsUsed = request.Location.IsUsed;
                        rs = await retailerRepository.UpdateLocation(location);
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                            if (request.Location.ImageData?.Length > 200)
                                CommonHelper.DeleteImage(oldImageUrl);
                        }
                        else
                        {
                            try
                            {
                                trans.Commit();
                            }
                            catch { }
                            CommonHelper.DeleteImage(request.Location.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
