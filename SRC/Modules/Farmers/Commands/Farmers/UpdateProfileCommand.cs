using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Farmers.ViewModels;
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
using Web.Helpers;

namespace Famrers.Commands.Commands.Famrers
{
    public class UpdateProfileCommand : BaseCommand<int>
    {
        public FarmerProfile Farmer { set; get; }

        public UpdateProfileCommand(FarmerProfile farmer)
        {
            Farmer = farmer;
        }
    }

    public class UpdateProfileCommandHandler : BaseCommandHandler<UpdateProfileCommand, int>
    {
        private readonly IFarmerRepository farmerRepository = null;
        private readonly IFarmerQueries farmerQueries = null;
        private readonly ILocationRepository locationRepository = null;
        private readonly IStorageQueries storageQueries = null;

        public UpdateProfileCommandHandler(IFarmerRepository farmerRepository,
            IFarmerQueries farmerQueries,
            ILocationRepository locationRepository,
            IStorageQueries storageQueries)
        {
            this.farmerRepository = farmerRepository;
            this.farmerQueries = farmerQueries;
            this.locationRepository = locationRepository;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            if (request.Farmer == null || request.Farmer.FarmerId == 0)
            {
                throw new BusinessException("Farmer.NotExisted");
            }

            var farmer = await farmerQueries.Get(request.Farmer.FarmerId);
            if (farmer == null)
            {
                throw new BusinessException("Farmer.NotExisted");
            }

            var farmerSession = await farmerQueries.GetByUser(request.LoginSession.Id);
            if (farmerSession == null || farmerSession.Id == 0 || farmerSession.Id != farmer.Id)
            {
                throw new NotPermissionException();
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
                        farmer.Address.Street = request.Farmer.Street;
                        farmer.Address.CountryId = request.Farmer.CountryId;
                        farmer.Address.ProvinceId = request.Farmer.ProvinceId;
                        farmer.Address.DistrictId = request.Farmer.DistrictId;
                        farmer.Address.WardId = request.Farmer.WardId;
                        //farmer.Address.Longitude = request.Farmer.Longitude;
                        //farmer.Address.Latitude = request.Farmer.Latitude;
                        farmer.Address = UpdateBuild(farmer.Address, request.LoginSession);
                        if (await locationRepository.AddOrUpdateAddress(farmer.Address) == -1)
                        {
                            return rs = -1;
                        }

                        farmer.Contact = farmer.Contact ?? new Contact();
                        farmer.Contact.Name = request.Farmer.Name;
                        farmer.Contact.Phone = request.Farmer.Phone;
                        farmer.Contact.Email = request.Farmer.Email;
                        farmer.Contact.Gender = request.Farmer.Gender;
                        farmer.Contact = UpdateBuild(farmer.Contact, request.LoginSession);

                        if (await locationRepository.AddOrUpdateContact(farmer.Contact) == -1)
                        {
                            return rs = -1;
                        }

                        farmer = UpdateBuild(farmer, request.LoginSession);
                        farmer.Name = request.Farmer.Name;
                        if (string.IsNullOrWhiteSpace(farmer.Code))
                            farmer.Code = (await storageQueries.GenarateCodeAsync(StorageKeys.FarmerCode));

                        rs = await farmerRepository.Update(farmer);
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
                        }
                    }
                }
            }

            return rs;
        }
    }
}
