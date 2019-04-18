using Common.Models;
using DAL;
using Dapper;
using GS1.UI.GTINs.Mappings;
using GS1.UI.GTINs.Models;
using GS1.UI.ProductionImages.Models;
using GS1.UI.ProductionImages.ViewModels;
using GS1.UI.Productions.Interfaces;
using GS1.UI.Productions.Models;
using GS1.UI.Productions.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS1.Productions.Queries
{
    public class ProductionQueries : BaseQueries, IProductionQueries
    {
        public async Task<IEnumerable<ProductionInformation>> GetAllAsync(int? partnerId = null)
        {
            string cmd = $@"SELECT p.*, pi.*, g.* FROM `production` p
                            LEFT JOIN `production_image` pi ON p.production_image_id = pi.id
                            LEFT JOIN `gtin` g ON p.gtin_id = g.id
                            WHERE p.`is_deleted` = 0";

            if ((partnerId ?? 0) > 0)
                cmd += $" AND p.`partner_id`='{partnerId ?? 0}'";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Production, ProductionImage, GTIN, ProductionInformation>(
                           (productionRs, imageRs, gTINRs) =>
                           {
                               ProductionInformation production = null;
                               if (productionRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(productionRs);
                                   production = JsonConvert.DeserializeObject<ProductionInformation>(serializedParent);
                               }
                               else production = new ProductionInformation();

                               if (imageRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(imageRs);
                                   production.ProductionImage = JsonConvert.DeserializeObject<ProductionImageData>(serializedParent);
                               }

                               if (gTINRs != null)
                                   production.GTIN = gTINRs.ToInformation();

                               return production;
                           }
                       );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<ProductionInformation> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT p.*, pi.*, g.* FROM `production` p
                            LEFT JOIN `production_image` pi ON p.production_image_id = pi.id
                            LEFT JOIN `gtin` g ON p.gtin_id = g.id
                            WHERE p.`id` = '{id}' and p.`is_deleted` = 0";
            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Production, ProductionImage, GTIN, ProductionInformation>(
                           (productionRs, imageRs, gTINRs) =>
                           {
                               ProductionInformation production = null;
                               if (productionRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(productionRs);
                                   production = JsonConvert.DeserializeObject<ProductionInformation>(serializedParent);
                               }
                               else production = new ProductionInformation();

                               if (imageRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(imageRs);
                                   production.ProductionImage = JsonConvert.DeserializeObject<ProductionImageData>(serializedParent);
                               }

                               if (gTINRs != null)
                                   production.GTIN = gTINRs.ToInformation();

                               return production;
                           }
                       ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<IEnumerable<ProductionInformation>> GetsAsync(int? partnerId = null)
        {
            string cmd = $@"SELECT p.*, pi.*, g.* FROM `production` p
                            LEFT JOIN `production_image` pi ON p.production_image_id = pi.id
                            LEFT JOIN `gtin` g ON p.gtin_id = g.id
                            WHERE p.`is_used` = 1 AND p.`is_deleted` = 0";

            if ((partnerId ?? 0) > 0)
                cmd += $" AND p.`partner_id`='{partnerId ?? 0}'";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Production, ProductionImage, GTIN, ProductionInformation>(
                           (productionRs, imageRs, gTINRs) =>
                           {
                               ProductionInformation production = null;
                               if (productionRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(productionRs);
                                   production = JsonConvert.DeserializeObject<ProductionInformation>(serializedParent);
                               }
                               else production = new ProductionInformation();

                               if (imageRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(imageRs);
                                   production.ProductionImage = JsonConvert.DeserializeObject<ProductionImageData>(serializedParent);
                               }

                               if (gTINRs != null)
                                   production.GTIN = gTINRs.ToInformation();

                               return production;
                           }
                       );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

    }
}
