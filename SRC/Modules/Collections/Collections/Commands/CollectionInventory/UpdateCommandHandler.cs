using Collections.UI;
using Common;
using Common.Exceptions;
using Common.Models;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Collections.Commands.CollectionInventory
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ICollectionInventoryRepository collectionInventoryRepository = null;
        private readonly ICollectionInventoryQueries collectionInventoryQueries = null;
        private readonly ICollectionQueries collectionQueries = null;
        private readonly ICollectionInventoryHistoryRepository collectionInventoryHistoryRepository = null;
        public UpdateCommandHandler(ICollectionInventoryRepository collectionInventoryRepository, 
                                    ICollectionInventoryQueries collectionInventoryQueries,
                                    ICollectionQueries collectionQueries,
                                    ICollectionInventoryHistoryRepository collectionInventoryHistoryRepository)
        {
            this.collectionInventoryRepository = collectionInventoryRepository;
            this.collectionInventoryQueries = collectionInventoryQueries;
            this.collectionQueries = collectionQueries;
            this.collectionInventoryHistoryRepository = collectionInventoryHistoryRepository;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if ((request.Direction != 1 && request.Direction != 0) || request.CollectionId == 0 || string.IsNullOrEmpty(request.TraceCode) || request.Quantity <= 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == request.CollectionId);
            if(collection == null)
            {
                throw new NotPermissionException();
            }

            var rs = -1;
            var inventory = await collectionInventoryQueries.GetByTraceCode(request.TraceCode);
            if(inventory == null)
            {
                if(request.Direction == 0)
                {
                    throw new BusinessException("Collection.Inventory.TraceCode.NotExisted");
                }
                using(var conn = DALHelper.GetConnection())
                {
                    conn.Open();
                    using(var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            collectionInventoryRepository.JoinTransaction(conn, trans);
                            collectionInventoryHistoryRepository.JoinTransaction(conn, trans);

                            await collectionInventoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventory()
                            {
                                CollectionId = request.CollectionId,
                                ProductId = request.ProductId,
                                UoMId = request.UoMId,
                                TraceCode  = request.TraceCode,
                                Quantity = request.Quantity,
                                ModifiedBy = request.LoginSession.Id,
                                ModifiedDate = DateTime.Now
                            });
                            await collectionInventoryHistoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventoryHistory()
                            {
                                TraceCode = request.TraceCode,
                                CollectionId = request.CollectionId,
                                ProductId = request.ProductId,
                                UoMId = request.UoMId,
                                Direction = request.Direction,
                                Quantity = request.Quantity,
                                LastQuantity = request.Quantity,
                                CreatedDate = DateTime.Now,
                                CreatedBy = request.LoginSession.Id,
                                Reason = request.Reason
                            });

                            return rs = 0;
                        }
                        finally
                        {
                            if(rs == 0)
                            {
                                trans.Commit();
                            }
                        }
                    }
                }
            }
            else
            {
                if(request.CollectionId != inventory.CollectionId)
                {
                    throw new BusinessException("Collection.Inventory.TraceCode.NotSameCollection");
                }

                using (var conn = DALHelper.GetConnection())
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            collectionInventoryRepository.JoinTransaction(conn, trans);
                            collectionInventoryHistoryRepository.JoinTransaction(conn, trans);

                            var quantity = inventory.Quantity + (request.Direction == 1 ? request.Quantity : -request.Quantity);
                            if(request.Direction == 0 && quantity < 0)
                            {
                                throw new BusinessException("Collection.Inventory.TraceCode.NotEnoughQuantity");
                            }

                            inventory.Quantity = quantity;
                            inventory.ModifiedDate = DateTime.Now;
                            inventory.ModifiedBy = request.LoginSession.Id;
                            await collectionInventoryRepository.Update(inventory);
                            await collectionInventoryHistoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventoryHistory()
                            {
                                TraceCode = request.TraceCode,
                                CollectionId = request.CollectionId,
                                ProductId = inventory.ProductId,
                                UoMId = inventory.UoMId,
                                Direction = request.Direction,
                                Quantity = request.Quantity,
                                LastQuantity = quantity,
                                CreatedDate = DateTime.Now,
                                CreatedBy = request.LoginSession.Id,
                                Reason = request.Reason
                            });

                            return rs = 0;
                        }
                        finally
                        {
                            if (rs == 0)
                            {
                                trans.Commit();
                            }
                        }
                    }
                }
            }
        }

        public override async Task<int> HandleCommandTransaction(UpdateCommand request, CachingCommandTransactionModel trans, CancellationToken cancellationToken)
        {
            if ((request.Direction != 1 && request.Direction != 2) || request.CollectionId == 0 || string.IsNullOrEmpty(request.TraceCode) || request.Quantity <= 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == request.CollectionId);
            if (collection == null)
            {
                throw new NotPermissionException();
            }


            collectionInventoryQueries.JoinTransaction(trans.Connection, trans.Transaction);
            collectionInventoryRepository.JoinTransaction(trans.Connection, trans.Transaction);
            collectionInventoryHistoryRepository.JoinTransaction(trans.Connection, trans.Transaction);

            var inventory = await collectionInventoryQueries.GetByTraceCode(request.TraceCode);
            if (inventory == null)
            {
                if (request.Direction == 0)
                {
                    throw new BusinessException("Collection.Inventory.TraceCode.NotExisted");
                }

                await collectionInventoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventory()
                {
                    CollectionId = request.CollectionId,
                    ProductId = request.ProductId,
                    UoMId = request.UoMId,
                    TraceCode = request.TraceCode,
                    Quantity = request.Quantity,
                    ModifiedBy = request.LoginSession.Id,
                    ModifiedDate = DateTime.Now
                });
                await collectionInventoryHistoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventoryHistory()
                {
                    TraceCode = request.TraceCode,
                    CollectionId = request.CollectionId,
                    ProductId = request.ProductId,
                    UoMId = request.UoMId,
                    Direction = request.Direction,
                    Quantity = request.Quantity,
                    LastQuantity = request.Quantity,
                    CreatedDate = DateTime.Now,
                    CreatedBy = request.LoginSession.Id,
                    Reason = request.Reason
                });
                
            }
            else
            {
                if (request.CollectionId != inventory.CollectionId)
                {
                    throw new BusinessException("Collection.Inventory.TraceCode.NotSameCollection");
                }

                var quantity = inventory.Quantity + (request.Direction == 1 ? request.Quantity : -request.Quantity);
                if (request.Direction == 0 && quantity < 0)
                {
                    throw new BusinessException("Collection.Inventory.TraceCode.NotEnoughQuantity");
                }

                if (quantity == 0)
                {
                    await collectionInventoryRepository.Delete(inventory.Id);
                }
                else
                {
                    inventory.Quantity = quantity;
                    inventory.ModifiedDate = DateTime.Now;
                    inventory.ModifiedBy = request.LoginSession.Id;
                    await collectionInventoryRepository.Update(inventory);
                }
                await collectionInventoryHistoryRepository.Add(new MDM.UI.Collections.Models.CollectionInventoryHistory()
                {
                    TraceCode = request.TraceCode,
                    CollectionId = request.CollectionId,
                    ProductId = request.ProductId,
                    UoMId = request.UoMId,
                    Direction = request.Direction,
                    Quantity = request.Quantity,
                    LastQuantity = quantity,
                    CreatedDate = DateTime.Now,
                    CreatedBy = request.LoginSession.Id,
                    Reason = request.Reason
                });
            }

            return 0;
        }
    }
}
