using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Collections.Commands.Collections
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ICollectionRepository collectionRepository = null;
        private readonly ICollectionQueries collectionQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public UpdateCommandHandler(ICollectionRepository collectionRepository, ICollectionQueries collectionQueries, ILocationRepository locationRepository)
        {
            this.collectionRepository = collectionRepository;
            this.collectionQueries = collectionQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Collection == null || request.Collection.Id == 0 || request.Collection.Address == null || request.Collection.Contact == null)
            {
                throw new BusinessException("Collection.NotExisted");
            }

            var collection = await collectionQueries.Get(request.Collection.Id);
            if (collection == null)
            {
                throw new BusinessException("Collection.NotExisted");
            }

            string oldImageUrl = request.Collection.ImageURL;
            if (request.Collection.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }
            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Collection.ImageData?.Length > 100)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Collection.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Collection.ImageData.Substring(request.Collection.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Collection.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.CollectionImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            var rs = -1;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        collectionRepository.JoinTransaction(conn, trans);
                        locationRepository.JoinTransaction(conn, trans);

                        collection.Address = collection.Address ?? new Address();
                        collection.Address.Street = request.Collection.Address.Street;
                        collection.Address.CountryId = request.Collection.Address.CountryId;
                        collection.Address.ProvinceId = request.Collection.Address.ProvinceId;
                        collection.Address.DistrictId = request.Collection.Address.DistrictId;
                        collection.Address.WardId = request.Collection.Address.WardId;
                        collection.Address.Longitude = request.Collection.Address.Longitude;
                        collection.Address.Latitude = request.Collection.Address.Latitude;
                        collection.Address = UpdateBuild(collection.Address, request.LoginSession);
                        if (await locationRepository.AddOrUpdateAddress(collection.Address) == -1)
                        {
                            return rs = -1;
                        }

                        collection.Contact = collection.Contact ?? new Contact();
                        collection.Contact.Name = request.Collection.Contact.Name;
                        collection.Contact.Phone = request.Collection.Contact.Phone;
                        collection.Contact.Email = request.Collection.Contact.Email;
                        collection.Contact.Gender = request.Collection.Contact.Gender;
                        collection.Contact = UpdateBuild(collection.Contact, request.LoginSession);
                        if (await locationRepository.AddOrUpdateContact(collection.Contact) == -1)
                        {
                            return rs = -1;
                        }

                        collection = UpdateBuild(collection, request.LoginSession);
                        collection.ImageURL = request.Collection.ImageURL;
                        collection.Name = request.Collection.Name;
                        collection.Code = request.Collection.Code;
                        collection.ManagerId = request.Collection.ManagerId;
                        collection.IsUsed = request.Collection.IsUsed;
                        rs = await collectionRepository.Update(collection);
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
                            CommonHelper.DeleteImage(request.Collection.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
