using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.Manager
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IDistributionRepository distributionRepository = null;
        private readonly IDistributionQueries distributionQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public AddCommandHandler(IDistributionRepository distributionRepository, IDistributionQueries distributionQueries, ILocationRepository locationRepository)
        {
            this.distributionRepository = distributionRepository;
            this.distributionQueries = distributionQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.Distribution == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

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

                        request.Distribution = CreateBuild(request.Distribution, request.LoginSession);
                        request.Distribution.AddressId = null;
                        request.Distribution.ContactId = null;
                        request.Distribution.Code = await distributionQueries.GenarateCode();
                        request.Distribution.Id = await distributionRepository.Add(request.Distribution);

                        request.Distribution.Address.Id = 0;
                        request.Distribution.Address.ObjectType = LocationOjectType.D.ToString();
                        request.Distribution.Address.ObjectId = request.Distribution.Id;
                        request.Distribution.Address.IsUsed = true;
                        request.Distribution.Address = CreateBuild(request.Distribution.Address, request.LoginSession);
                        var addressId = await locationRepository.AddOrUpdateAddress(request.Distribution.Address);

                        request.Distribution.Contact.Id = 0;
                        request.Distribution.Contact.ObjectType = LocationOjectType.D.ToString();
                        request.Distribution.Contact.ObjectId = request.Distribution.Id;
                        request.Distribution.Contact.IsUsed = true;
                        request.Distribution.Contact = CreateBuild(request.Distribution.Contact, request.LoginSession);
                        var contactId = await locationRepository.AddOrUpdateContact(request.Distribution.Contact);

                        request.Distribution.AddressId = addressId;
                        request.Distribution.ContactId = contactId;


                        rs = await distributionRepository.Update(request.Distribution);
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
                                trans.Rollback();
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
