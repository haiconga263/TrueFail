using Collections.UI;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Common.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Collections.Commands.CollectionShippings
{
    public class UpdateItemCommandHandler : BaseCommandHandler<UpdateItemCommand, int>
    {
        private readonly ICFShippingRepository cFShippingRepository = null;
        private readonly ICFShippingQueries cFShippingQueries = null;
        private readonly ICollectionQueries collectionQueries = null;
        public UpdateItemCommandHandler(ICFShippingRepository cFShippingRepository, ICFShippingQueries cFShippingQueries, ICollectionQueries collectionQueries)
        {
            this.cFShippingRepository = cFShippingRepository;
            this.collectionQueries = collectionQueries;
            this.cFShippingQueries = cFShippingQueries;
        }
        public override async Task<int> HandleCommand(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            if (request.ShippingId == 0 || request.Items == null && request.Items.Count == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var shipping = await cFShippingQueries.Get(request.ShippingId);
            if(shipping == null)
            {
                throw new BusinessException("Trip.NotExisted");
            }

            if (shipping.StatusId != (int)Distributions.UI.TripStatuses.Created)
            {
                throw new BusinessException("Common.ErrorWithStep");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == shipping.CollectionId);
            if (collection == null)
            {
                throw new NotPermissionException();
            }

            var rs = -1;
            using(var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {
                    try
                    {
                        await cFShippingRepository.DeleteItems(shipping.Id);
                        foreach (var item in request.Items)
                        {
                            if(item.ShippedQuantity == 0)
                            {
                                throw new BusinessException("");
                            }
                            item.ShippingId = shipping.Id;
                            item.DeliveriedQuantity = 0;
                            await cFShippingRepository.AddItem(item);
                        }

                        shipping = UpdateBuild(shipping, request.LoginSession);
                        rs = await cFShippingRepository.Update(shipping);
                    }
                    finally
                    {
                        if(rs == 0)
                        {
                            tran.Commit();
                        }
                        else
                        {
                            try
                            {
                                tran.Rollback();
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
