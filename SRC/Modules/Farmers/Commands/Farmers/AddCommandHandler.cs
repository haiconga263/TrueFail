using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Geographical.Interfaces;
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
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IFarmerRepository farmerRepository = null;
        private readonly ILocationRepository locationRepository = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(IFarmerRepository farmerRepository,
            ILocationRepository locationRepository = null,
            IStorageQueries storageQueries = null)
        {
            this.farmerRepository = farmerRepository;
            this.locationRepository = locationRepository;
            this.storageQueries = storageQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Farmer == null || request.Farmer.Address == null || request.Farmer.Contact == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if (request.Farmer.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }
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

                        request.Farmer = CreateBuild(request.Farmer, request.LoginSession);
                        if (!request.Farmer.IsCompany)
                        {
                            request.Farmer.TaxCode = string.Empty;
                        }
                        request.Farmer.Code = (await storageQueries.GenarateCodeAsync(StorageKeys.FarmerCode));

                        request.Farmer.Id = await farmerRepository.Add(request.Farmer);

                        request.Farmer.Address.Id = 0;
                        request.Farmer.Address.ObjectType = LocationOjectType.F.ToString();
                        request.Farmer.Address.ObjectId = request.Farmer.Id;
                        request.Farmer.Address.IsUsed = true;
                        request.Farmer.Address = CreateBuild(request.Farmer.Address, request.LoginSession);
                        var addressId = await locationRepository.AddOrUpdateAddress(request.Farmer.Address);

                        request.Farmer.Contact.Id = 0;
                        request.Farmer.Contact.ObjectType = LocationOjectType.F.ToString();
                        request.Farmer.Contact.ObjectId = request.Farmer.Id;
                        request.Farmer.Contact.IsUsed = true;
                        request.Farmer.Contact = CreateBuild(request.Farmer.Contact, request.LoginSession);
                        var contactId = await locationRepository.AddOrUpdateContact(request.Farmer.Contact);

                        request.Farmer.AddressId = addressId;
                        request.Farmer.ContactId = contactId;


                        rs = await farmerRepository.Update(request.Farmer);
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
                                trans.Commit();
                            }
                            catch { }
                            CommonHelper.DeleteImage(request.Farmer.ImageURL);
                        }
                    }
                }
            };

            return rs == 0 ? -1 : 0;
        }
    }
}
