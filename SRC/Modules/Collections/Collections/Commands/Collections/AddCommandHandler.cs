using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Collections.Commands.Collections
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ICollectionRepository collectionRepository = null;
        private readonly ICollectionQueries collectionQueries = null;
        private readonly ILocationRepository locationRepository = null;
        public AddCommandHandler(ICollectionRepository collectionRepository, ICollectionQueries collectionQueries, ILocationRepository locationRepository)
        {
            this.collectionRepository = collectionRepository;
            this.collectionQueries = collectionQueries;
            this.locationRepository = locationRepository;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.Collection == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

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

                        request.Collection.Code = await collectionQueries.GenarateCode();
                        request.Collection = CreateBuild(request.Collection, request.LoginSession);
                        request.Collection.AddressId = null;
                        request.Collection.ContactId = null;
                        request.Collection.Id = await collectionRepository.Add(request.Collection);

                        request.Collection.Address.Id = 0;
                        request.Collection.Address.ObjectType = LocationOjectType.C.ToString();
                        request.Collection.Address.ObjectId = request.Collection.Id;
                        request.Collection.Address.IsUsed = true;
                        request.Collection.Address = CreateBuild(request.Collection.Address, request.LoginSession);
                        var addressId = await locationRepository.AddOrUpdateAddress(request.Collection.Address);

                        request.Collection.Contact.Id = 0;
                        request.Collection.Contact.ObjectType = LocationOjectType.C.ToString();
                        request.Collection.Contact.ObjectId = request.Collection.Id;
                        request.Collection.Contact.IsUsed = true;
                        request.Collection.Contact = CreateBuild(request.Collection.Contact, request.LoginSession);
                        var contactId = await locationRepository.AddOrUpdateContact(request.Collection.Contact);

                        request.Collection.AddressId = addressId;
                        request.Collection.ContactId = contactId;


                        rs = await collectionRepository.Update(request.Collection);
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
                            CommonHelper.DeleteImage(request.Collection.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
