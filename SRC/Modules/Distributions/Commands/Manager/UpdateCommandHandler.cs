using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.Manager
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IDistributionRepository distributionRepository = null;
        private readonly IDistributionQueries distributionQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public UpdateCommandHandler(IDistributionRepository distributionRepository, IDistributionQueries distributionQueries, ILocationRepository locationRepository)
        {
            this.distributionRepository = distributionRepository;
            this.distributionQueries = distributionQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Distribution == null || request.Distribution.Id == 0 || request.Distribution.Address == null || request.Distribution.Contact == null)
            {
                throw new BusinessException("Distribution.NotExisted");
            }

            var distribution = await distributionQueries.Get(request.Distribution.Id);
            if (distribution == null)
            {
                throw new BusinessException("Distribution.NotExisted");
            }

            string oldImageUrl = request.Distribution.ImageURL;
            if (request.Distribution.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }
            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Distribution.ImageData?.Length > 100)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Distribution.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Distribution.ImageData.Substring(request.Distribution.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Distribution.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.DistributionImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            var rs = -1;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        distributionRepository.JoinTransaction(conn, trans);
                        locationRepository.JoinTransaction(conn, trans);

                        distribution.Address = distribution.Address ?? new Address();
                        distribution.Address.Street = request.Distribution.Address.Street;
                        distribution.Address.CountryId = request.Distribution.Address.CountryId;
                        distribution.Address.ProvinceId = request.Distribution.Address.ProvinceId;
                        distribution.Address.DistrictId = request.Distribution.Address.DistrictId;
                        distribution.Address.WardId = request.Distribution.Address.WardId;
                        distribution.Address.Longitude = request.Distribution.Address.Longitude;
                        distribution.Address.Latitude = request.Distribution.Address.Latitude;
                        distribution.Address = UpdateBuild(distribution.Address, request.LoginSession);
                        if (await locationRepository.AddOrUpdateAddress(distribution.Address) == -1)
                        {
                            return rs = -1;
                        }

                        distribution.Contact = distribution.Contact ?? new Contact();
                        distribution.Contact.Name = request.Distribution.Contact.Name;
                        distribution.Contact.Phone = request.Distribution.Contact.Phone;
                        distribution.Contact.Email = request.Distribution.Contact.Email;
                        distribution.Contact.Gender = request.Distribution.Contact.Gender;
                        distribution.Contact = UpdateBuild(distribution.Contact, request.LoginSession);
                        if (await locationRepository.AddOrUpdateContact(distribution.Contact) == -1)
                        {
                            return rs = -1;
                        }

                        distribution = UpdateBuild(distribution, request.LoginSession);
                        distribution.ImageURL = request.Distribution.ImageURL;
                        distribution.Name = request.Distribution.Name;
                        distribution.Code = request.Distribution.Code;
                        distribution.Radius = request.Distribution.Radius;
                        distribution.ManagerId = request.Distribution.ManagerId;
                        distribution.IsUsed = request.Distribution.IsUsed;
                        rs = await distributionRepository.Update(distribution);
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
                            CommonHelper.DeleteImage(request.Distribution.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
