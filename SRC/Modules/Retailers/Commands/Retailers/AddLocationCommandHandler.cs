using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Retailers.Interfaces;
using MDM.UI.Retailers.Models;
using Retailers.UI;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;
using MDM.UI.Distributions.ViewModels;
using System.Collections.Generic;

namespace Retailers.Commands.Commands.Retailers
{
    public class AddLocationCommandHandler : BaseCommandHandler<AddLocationCommand, int>
    {
        private readonly IRetailerRepository retailerRepository = null;
        private readonly IRetailerQueries retailerQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public AddLocationCommandHandler(IRetailerRepository retailerRepository, IRetailerQueries retailerQueries, ILocationRepository locationRepository = null)
        {
            this.retailerRepository = retailerRepository;
            this.retailerQueries = retailerQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(AddLocationCommand request, CancellationToken cancellationToken)
        {
            if (request.Location == null  || request.Location.Address == null || request.Location.Contact == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if (request.LoginSession.Roles.FirstOrDefault(r => r == "Administrator") == null)
            {
                var retailer = await retailerQueries.GetByUserId(request.LoginSession.Id);
                if (retailer == null)
                {
                    throw new BusinessException("Retailer.NotExisted");
                }
                request.Location.RetailerId = retailer.Id;
            }
            else
            {
                if (request.Location.RetailerId == 0)
                {
                    throw new BusinessException("AddWrongInformation");
                }
                else
                {
                    var retailer = await retailerQueries.Get(request.Location.RetailerId);
                    if (retailer == null)
                    {
                        throw new BusinessException("Retailer.NotExisted");
                    }
                }
            }

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
                        retailerQueries.JoinTransaction(conn, trans);
                        locationRepository.JoinTransaction(conn, trans);

                        request.Location.GLN = await retailerQueries.GenarateLocationCode();
                        request.Location = CreateBuild(request.Location, request.LoginSession);
                        request.Location.Id = await retailerRepository.AddLocation(request.Location);

                        request.Location.Address.Id = 0;
                        request.Location.Address.ObjectType = LocationOjectType.R.ToString();
                        request.Location.Address.ObjectId = request.Location.RetailerId;
                        request.Location.Address.IsUsed = true;
                        request.Location.Address = CreateBuild(request.Location.Address, request.LoginSession);
                        var addressId = await locationRepository.AddOrUpdateAddress(request.Location.Address);

                        request.Location.Contact.Id = 0;
                        request.Location.Contact.ObjectType = LocationOjectType.R.ToString();
                        request.Location.Contact.ObjectId = request.Location.RetailerId;
                        request.Location.Contact.IsUsed = true;
                        request.Location.Contact = CreateBuild(request.Location.Contact, request.LoginSession);
                        var contactId = await locationRepository.AddOrUpdateContact(request.Location.Contact);

                        request.Location.AddressId = addressId;
                        request.Location.ContactId = contactId;

                        rs = await retailerRepository.UpdateLocation(request.Location);
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
                            CommonHelper.DeleteImage(request.Location.ImageURL);
                        }
                    }
                }
            };

            return rs;
        }
    }
}
