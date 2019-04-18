using Common;
using Common.Exceptions;
using DAL;
using Distributions.UI;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Common.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Collections.Commands.CollectionShippings
{
    public class UpdateStatusCommandHandler : BaseCommandHandler<UpdateStatusCommand, int>
    {
        private readonly ICFShippingRepository cFShippingRepository = null;
        private readonly ICFShippingQueries cFShippingQueries = null;
        private readonly ICollectionQueries collectionQueries = null;
        private readonly ICollectionInventoryRepository collectionInventoryRepository = null;
        private readonly ICollectionInventoryQueries collectionInventoryQueries = null;
        private readonly ICollectionInventoryHistoryRepository collectionInventoryHistoryRepository = null;
        public UpdateStatusCommandHandler(ICFShippingRepository cFShippingRepository, 
                                          ICFShippingQueries cFShippingQueries, 
                                          ICollectionQueries collectionQueries, 
                                          ICollectionInventoryRepository collectionInventoryRepository, 
                                          ICollectionInventoryQueries collectionInventoryQueries,
                                          ICollectionInventoryHistoryRepository collectionInventoryHistoryRepository)
        {
            this.cFShippingRepository = cFShippingRepository;
            this.collectionQueries = collectionQueries;
            this.cFShippingQueries = cFShippingQueries;
            this.collectionInventoryRepository = collectionInventoryRepository;
            this.collectionInventoryQueries = collectionInventoryQueries;
            this.collectionInventoryHistoryRepository = collectionInventoryHistoryRepository;
        }
        public override async Task<int> HandleCommand(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.ShippingId == 0 || request.StatusId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if (!Enum.IsDefined(typeof(TripStatuses), (int)request.StatusId))
            {
                throw new BusinessException("Trip.NotExistedStatus");
            }
            var status = (TripStatuses)request.StatusId;

            var shipping = await cFShippingQueries.Get(request.ShippingId);
            if (shipping == null)
            {
                throw new BusinessException("Trip.NotExisted");
            }

            if(shipping.StatusId == (int)TripStatuses.Finished || shipping.StatusId == (int)TripStatuses.Canceled)
            {
                throw new BusinessException("Trip.WrongStep");
            }

            if (status != TripStatuses.Canceled)
            {
                if (request.StatusId != shipping.StatusId + 1)
                {
                    throw new BusinessException("Trip.WrongStep");
                }
            }

            if (status == TripStatuses.Confirmed || (status == TripStatuses.Canceled && shipping.StatusId != (int)TripStatuses.Created))
            {
                var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, UI.AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
                if (employee == null)
                {
                    throw new NotPermissionException();
                }
                var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == shipping.CollectionId);
                if (collection == null)
                {
                    throw new NotPermissionException();
                }

                if (status == TripStatuses.Confirmed && shipping.ShipperId == null)
                {
                    throw new BusinessException("Distribution.Trip.RequiredDeliveryMan");
                }

                using (var conn = DALHelper.GetConnection())
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        var rs = -1;
                        try
                        {
                            cFShippingQueries.JoinTransaction(conn, trans);
                            cFShippingRepository.JoinTransaction(conn, trans);
                            collectionInventoryQueries.JoinTransaction(conn, trans);
                            collectionInventoryHistoryRepository.JoinTransaction(conn, trans);

                            var items = await cFShippingQueries.GetItems(shipping.Id);
                            if(items.Count() == 0)
                            {
                                throw new BusinessException("Collection.Shipping.NotExsitedItem");
                            }
                            foreach (var item in items)
                            {
                                var inventory = await collectionInventoryQueries.GetByTraceCode(item.TraceCode);
                                if (status == TripStatuses.Confirmed)
                                {
                                    if (inventory == null || inventory.CollectionId != shipping.CollectionId)
                                    {
                                        throw new BusinessException("Collection.Inventory.TraceCode.NotExisted");
                                    }

                                    if(inventory.Quantity < item.ShippedQuantity)
                                    {
                                        throw new BusinessException("Collection.Inventory.TraceCode.NotEnoughQuantity");
                                    }

                                    inventory.Quantity -= item.ShippedQuantity;
                                    if (inventory.Quantity == 0)
                                    {
                                        await collectionInventoryRepository.Delete(inventory.Id);
                                    }
                                    else
                                    {
                                        inventory.ModifiedDate = DateTime.Now;
                                        inventory.ModifiedBy = request.LoginSession.Id;
                                        await collectionInventoryRepository.Update(inventory);
                                    }
                                    await collectionInventoryHistoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventoryHistory()
                                    {
                                        TraceCode = inventory.TraceCode,
                                        CollectionId = inventory.CollectionId,
                                        ProductId = inventory.ProductId,
                                        UoMId = inventory.UoMId,
                                        Direction = 0,
                                        Quantity = item.ShippedQuantity,
                                        LastQuantity = inventory.Quantity,
                                        CreatedDate = DateTime.Now,
                                        CreatedBy = request.LoginSession.Id
                                    });
                                }
                                else
                                {
                                    if(inventory == null)
                                    {
                                        await collectionInventoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventory()
                                        {
                                            CollectionId = shipping.CollectionId,
                                            ProductId = item.ProductId,
                                            UoMId = item.UoMId,
                                            TraceCode = item.TraceCode,
                                            Quantity = item.ShippedQuantity,
                                            ModifiedBy = request.LoginSession.Id,
                                            ModifiedDate = DateTime.Now
                                        });
                                    }
                                    else
                                    {
                                        inventory.Quantity += item.ShippedQuantity;
                                        inventory.ModifiedDate = DateTime.Now;
                                        inventory.ModifiedBy = request.LoginSession.Id;
                                        await collectionInventoryRepository.Update(inventory);
                                    }

                                    await collectionInventoryHistoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventoryHistory()
                                    {
                                        TraceCode = item.TraceCode,
                                        CollectionId = shipping.CollectionId,
                                        ProductId = item.ProductId,
                                        UoMId = item.UoMId,
                                        Direction = 1,
                                        Quantity = item.ShippedQuantity,
                                        LastQuantity = inventory == null ? item.ShippedQuantity : item.ShippedQuantity + inventory.Quantity,
                                        CreatedDate = DateTime.Now,
                                        CreatedBy = request.LoginSession.Id
                                    });
                                }
                            }

                            shipping = UpdateBuild(shipping, request.LoginSession);
                            shipping.StatusId = request.StatusId;
                            return rs = await cFShippingRepository.Update(shipping);
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
                            }
                        }
                    }
                }
            }
            else
            {
                shipping = UpdateBuild(shipping, request.LoginSession);
                shipping.StatusId = request.StatusId;
                return await cFShippingRepository.Update(shipping);
            }
        }
    }
}
