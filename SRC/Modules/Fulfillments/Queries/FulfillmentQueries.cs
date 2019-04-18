using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Fulfillments.UI.Interfaces;
using Fulfillments.UI.Models;
using Fulfillments.UI.ViewModels;
using MDM.UI.Collections.Models;
using MDM.UI.Common.Models;
using MDM.UI.Fulfillments.Models;
using MDM.UI.Fulfillments.ViewModels;
using MDM.UI.Products.Models;
using MDM.UI.UoMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.Queries
{
    public class FulfillmentQueries : BaseQueries, IFulfillmentCollectionQueries
    {
        public async Task<IEnumerable<Collection>> GetCollection()
        {
            List<Collection> result = new List<Collection>();
            string cmd = $@"SELECT * FROM `collection`                             
                            WHERE is_used = 1 AND is_deleted = 0";
            return await DALHelper.Query<Collection>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<FulfillmentViewModel>> GetFulfillment()
        {
            List<FulfillmentViewModel> result = new List<FulfillmentViewModel>();
            string cmd = $@"SELECT * FROM `fulfillment`                             
                            WHERE is_deleted = 0";
            return await DALHelper.Query<FulfillmentViewModel>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<FulfillmentCollectionViewModel>> GetOrderFromCollection()
        {
            List<FulfillmentCollectionViewModel> result = new List<FulfillmentCollectionViewModel>();
            string cmd = $@"SELECT *
                            FROM `cf_shipping` sp
                            LEFT JOIN `collection` co ON co.id = sp.collection_id AND co.is_used = 1 AND co.is_deleted = 0
                            LEFT JOIN `fulfillment` fu ON fu.id = sp.fulfillment_id AND fu.is_used = 1 AND fu.is_deleted = 0
                            LEFT JOIN `cf_shipping_item` spi ON sp.id = spi.cf_shipping_id                              
                            LEFT JOIN `product` p ON p.id = spi.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = spi.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE sp.is_deleted = 0 and sp.status_id = 3" ;

            DbConnection = DbConnection ?? DALHelper.GetConnection();
            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<FulfillmentCollection, Collection, Fulfillment, FulfillmentCollectionItem, Product, UoM, FulfillmentCollectionViewModel>(
                (fc, col, fu, fci, pro, Uom) =>
                {
                  
                    FulfillmentCollectionViewModel vm = result.FirstOrDefault(x => x.ID == fc.ID);
                    if (vm == null)
                    {
                        vm = CommonHelper.Mapper<FulfillmentCollection, FulfillmentCollectionViewModel>(fc);
                        result.Add(vm);
                    }

                    if (vm.Collection == null)
                    {
                        vm.Collection = col;
                    }

                    if (vm.Fulfillment == null)
                    {
                        vm.Fulfillment = fu;
                    }

                    var item = vm.Items.FirstOrDefault(i => i.Id == fci.Id);
                    if (item == null && fci != null)
                    {
                        item = CommonHelper.Mapper<FulfillmentCollectionItem, FulfillmentCollectionItemViewModel>(fci);
                        vm.Items.Add(item);
                    }

                    if (item != null)
                    {
                        item.Product = pro;
                        item.UoM = Uom;
                    }

                    return vm;
                }
            );

            return result;
        }

        public async Task<IEnumerable<FulfillmentCollectionViewModel>> GetOrderFromCollectionById(string id)
        {
            List<FulfillmentCollectionViewModel> result = new List<FulfillmentCollectionViewModel>();
            string cmd = $@"SELECT sp.*, co.*,fu.*, spi.*, p.*, u.*
                            FROM `cf_shipping` sp
                            LEFT JOIN `collection` co ON co.id = sp.collection_id AND co.is_used = 1 AND co.is_deleted = 0
                            LEFT JOIN `fulfillment` fu ON fu.id = sp.fulfillment_id AND fu.is_used = 1 AND fu.is_deleted = 0
                            LEFT JOIN `cf_shipping_item` spi ON sp.id = spi.cf_shipping_id  AND sp.is_deleted = 0
                            LEFT JOIN `product` p ON p.id = spi.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = spi.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE sp.collection_id = '{id} AND sp.status_id = 3";


            DbConnection = DbConnection ?? DALHelper.GetConnection();
            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<FulfillmentCollection, Collection, Fulfillment, FulfillmentCollectionItem, Product, UoM, FulfillmentCollectionViewModel>(
                (fc, col, fu, fci, pro, Uom) =>
                {

                    FulfillmentCollectionViewModel vm = result.FirstOrDefault(x => x.ID == fc.ID);
                    if (vm == null)
                    {
                        vm = CommonHelper.Mapper<FulfillmentCollection, FulfillmentCollectionViewModel>(fc);
                        result.Add(vm);
                    }

                    if (vm.Collection == null)
                    {
                        vm.Collection = col;
                    }

                    if (vm.Fulfillment == null)
                    {
                        vm.Fulfillment = fu;
                    }

                    var item = vm.Items.FirstOrDefault(i => i.Id == fci.Id);
                    if (item == null && fci != null)
                    {
                        item = CommonHelper.Mapper<FulfillmentCollectionItem, FulfillmentCollectionItemViewModel>(fci);
                        vm.Items.Add(item);
                    }

                    if (item != null)
                    {
                        item.Product = pro;
                        item.UoM = Uom;
                    }

                    return vm;
                }
            );

            return result;
        }

        public async Task<IEnumerable<FCViewModel>> GetOrderFromCollectionByFcId(string id)
        {
            List<FCViewModel> result = new List<FCViewModel>();
            string cmd = $@"SELECT fuco.*, co.*,fu.*, fucoi.*, p.*, u.*
                            FROM `fulfillment_collection` fuco
                            LEFT JOIN `collection` co ON co.id = fuco.collection_id AND co.is_used = 1 AND co.is_deleted = 0
                            LEFT JOIN `fulfillment` fu ON fu.id = fuco.fulfillment_id AND fu.is_used = 1 AND fu.is_deleted = 0
                            LEFT JOIN `fulfillment_collection_item` fucoi ON fuco.id = fucoi.fulfillment_collection_id 
                            LEFT JOIN `product` p ON p.id = fucoi.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = fucoi.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE fuco.fulfillment_id = '{id}'
                            ";

            DbConnection = DbConnection ?? DALHelper.GetConnection();
            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<FulfillmentCollection, Collection, Fulfillment, CFShippingItem, Product, UoM, FCViewModel>(
                (fc, col, fu, fci, pro, Uom) =>
                {

                    FCViewModel vm = result.FirstOrDefault(x => x.ID == fc.ID);
                    if (vm == null)
                    {
                        vm = CommonHelper.Mapper<FulfillmentCollection, FCViewModel>(fc);
                        result.Add(vm);
                    }

                    if (vm.Collection == null)
                    {
                        vm.Collection = col;
                    }

                    if (vm.Fulfillment == null)
                    {
                        vm.Fulfillment = fu;
                    }

                    var item = vm.Items.FirstOrDefault(i => i.Id == fci.Id);
                    if (item == null && fci != null)
                    {
                        item = CommonHelper.Mapper<CFShippingItem, FCItemViewModel>(fci);
                        vm.Items.Add(item);
                    }

                    if (item != null)
                    {
                        item.Product = pro;
                        item.UoM = Uom;
                    }

                    return vm;
                }
            );

            return result;
        }

        public async Task<IEnumerable<FulfillmentCollectionStatus>> GetFulfillmentCollectionStatus()
        {
            List<FulfillmentCollectionStatus> result = new List<FulfillmentCollectionStatus>();
            string cmd = $@"SELECT * FROM `fulfillment_collection_status`";
            return await DALHelper.Query<FulfillmentCollectionStatus>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<Product>> GetAllFCProduct()
        {
            List<Product> result = new List<Product>();
            string cmd = $@"SELECT DISTINCT pro.*
                            FROM `fulfillment_collection` fc
                            JOIN `fulfillment_collection_item` fci ON fc.id = fci.fulfillment_collection_id
                            JOIN `product` pro ON fci.product_id = pro.id";
            return await DALHelper.Query<Product>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }


    }
}
