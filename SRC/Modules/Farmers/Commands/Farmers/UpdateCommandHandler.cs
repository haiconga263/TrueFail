using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Famrers.Commands.Commands.Famrers
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IFarmerRepository farmerRepository = null;
        private readonly IFarmerQueries farmerQueries = null;
        private readonly ILocationRepository locationRepository = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateCommandHandler(IFarmerRepository farmerRepository,
            IFarmerQueries farmerQueries,
            ILocationRepository locationRepository,
            IStorageQueries storageQueries)
        {
            this.farmerRepository = farmerRepository;
            this.farmerQueries = farmerQueries;
            this.locationRepository = locationRepository;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Farmer == null || request.Farmer.Id == 0 || request.Farmer.Address == null || request.Farmer.Contact == null)
            {
                throw new BusinessException("Farmer.NotExisted");
            }

            var farmer = await farmerQueries.Get(request.Farmer.Id);
            if (farmer == null)
            {
                throw new BusinessException("Farmer.NotExisted");
            }

            string oldImageUrl = request.Farmer.ImageURL;

            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Farmer.ImageData?.Length > 100)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Farmer.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Farmer.ImageData.Substring(request.Farmer.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Farmer.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.FarmerImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            var rs = -1;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        farmerRepository.JoinTransaction(conn, trans);
                        locationRepository.JoinTransaction(conn, trans);

                        farmer.Address = farmer.Address ?? new Address();
                        farmer.Address.Street = request.Farmer.Address.Street;
                        farmer.Address.CountryId = request.Farmer.Address.CountryId;
                        farmer.Address.ProvinceId = request.Farmer.Address.ProvinceId;
                        farmer.Address.DistrictId = request.Farmer.Address.DistrictId;
                        farmer.Address.WardId = request.Farmer.Address.WardId;
                        farmer.Address.Longitude = request.Farmer.Address.Longitude;
                        farmer.Address.Latitude = request.Farmer.Address.Latitude;
                        farmer.Address = UpdateBuild(farmer.Address, request.LoginSession);
                        if (await locationRepository.AddOrUpdateAddress(farmer.Address) == -1)
                        {
                            return rs = -1;
                        }

                        farmer.Contact = farmer.Contact ?? new Contact();
                        farmer.Contact.Name = request.Farmer.Contact.Name;
                        farmer.Contact.Phone = request.Farmer.Contact.Phone;
                        farmer.Contact.Email = request.Farmer.Contact.Email;
                        farmer.Contact.Gender = request.Farmer.Contact.Gender;
                        farmer.Contact = UpdateBuild(farmer.Contact, request.LoginSession);
                        if (await locationRepository.AddOrUpdateContact(farmer.Contact) == -1)
                        {
                            return rs = -1;
                        }

                        farmer = UpdateBuild(farmer, request.LoginSession);
                        farmer.ImageURL = request.Farmer.ImageURL;
                        farmer.Name = request.Farmer.Name;
                        farmer.UserId = request.Farmer.UserId;
                        farmer.IsUsed = request.Farmer.IsUsed;
                        farmer.IsCompany = request.Farmer.IsCompany;

                        if (string.IsNullOrWhiteSpace(farmer.Code))
                            farmer.Code = (await storageQueries.GenarateCodeAsync(StorageKeys.FarmerCode));

                        if (!request.Farmer.IsCompany)
                        {
                            farmer.TaxCode = string.Empty;
                        }
                        else
                        {
                            farmer.TaxCode = request.Farmer.TaxCode;
                        }
                        rs = await farmerRepository.Update(farmer);
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
                            CommonHelper.DeleteImage(request.Farmer.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
