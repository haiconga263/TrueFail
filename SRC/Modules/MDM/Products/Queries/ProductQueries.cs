using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Categories.Models;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Products.Queries
{
    public class ProductQueries : BaseQueries, IProductQueries
    {
        private const string ProductCodeFormat = "P{0}";
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `product`");
            if (previousCode == null)
            {
                code = ProductCodeFormat.Replace("{0}", 1.ToString("000000000"));
            }
            else
            {
                code = ProductCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(1, 9)) + 1).ToString("000000000"));
            }

            return code;
        }

        public async Task<IEnumerable<ProductViewModel>> Get(int productId, int languageId = 1, DateTime? timeGet = null)
        {
            if (timeGet == null)
            {
                timeGet = DateTime.Now;
            }

            List<ProductViewModel> result = new List<ProductViewModel>();
            string cmd = $@"SELECT * FROM `product` p
                            LEFT JOIN `category` c ON c.id = p.category_id AND c.is_used = 1
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = {languageId}
                            LEFT JOIN `product_price` pp ON p.id = pp.product_id and pp.effectived_date = (SELECT max(ppp.effectived_date) FROM `product_price` ppp 
                                                                                  WHERE ppp.product_id = p.id AND ppp.effectived_date <= '{timeGet.Value.ToString("yyyyMMdd")}'
                                                                                  ORDER BY ppp.effectived_date desc limit 1)
                            WHERE p.id = {productId} and p.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                    (pRs, cRs, plRs, ppRs) =>
                    {
                        var product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                        result.Add(product);

                        if (product.Category == null)
                        {
                            product.Category = cRs;
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;

                            var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                            if (lang == null)
                            {
                                product.Languages.Add(plRs);
                            }
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            product.CurrentUoM = ppRs.UoMId;
                            product.BuyingCurrentPrice = ppRs.BuyingPrice;
                            product.SellingCurrentPrice = ppRs.SellingPrice;
                            product.CurrentWeight = ppRs.Weight;
                            product.CurrentCapacity = ppRs.Capacity;

                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            product.CurrentUoM = product.DefaultUoMId;
                            product.BuyingCurrentPrice = product.DefaultBuyingPrice;
                            product.SellingCurrentPrice = product.DefaultSellingPrice;
                        }

                        return product;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                        (pRs, cRs, plRs, ppRs) =>
                        {
                            var product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                            result.Add(product);

                            if (product.Category == null)
                            {
                                product.Category = cRs;
                            }

                            if (plRs != null)
                            {
                                product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                                product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;

                                var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                                if (lang == null)
                                {
                                    product.Languages.Add(plRs);
                                }
                            }
                            else
                            {
                                product.Name = product.DefaultName;
                                product.Decription = product.DefaultDescription;
                            }

                            if (ppRs != null)
                            {
                                product.CurrentUoM = ppRs.UoMId;
                                product.BuyingCurrentPrice = ppRs.BuyingPrice;
                                product.SellingCurrentPrice = ppRs.SellingPrice;
                                product.CurrentWeight = ppRs.Weight;
                                product.CurrentCapacity = ppRs.Capacity;

                                var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    product.Prices.Add(ppRs);
                                }
                            }
                            else
                            {
                                product.CurrentUoM = product.DefaultUoMId;
                                product.BuyingCurrentPrice = product.DefaultBuyingPrice;
                                product.SellingCurrentPrice = product.DefaultSellingPrice;
                            }

                            return product;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<ProductViewModel> GetFull(int productId)
        {
            ProductViewModel result = null;
            string cmd = $@"SELECT * FROM `product` p
                            LEFT JOIN `category` c ON c.id = p.category_id AND c.is_used = 1
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id
                            LEFT JOIN `product_price` pp ON pp.product_id = p.id
                            WHERE p.id = {productId} and p.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                    (pRs, cRs, plRs, ppRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                        }

                        if (result.Category == null)
                        {
                            result.Category = cRs;
                        }

                        if (plRs != null)
                        {
                            var lang = result.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                            if (lang == null)
                            {
                                result.Languages.Add(plRs);
                            }
                        }

                        if (ppRs != null)
                        {
                            var price = result.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                result.Prices.Add(ppRs);
                            }
                        }

                        return result;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                        (pRs, cRs, plRs, ppRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                            }

                            if (result.Category == null)
                            {
                                result.Category = cRs;
                            }

                            if (plRs != null)
                            {
                                var lang = result.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                                if (lang == null)
                                {
                                    result.Languages.Add(plRs);
                                }
                            }

                            if (ppRs != null)
                            {
                                var price = result.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    result.Prices.Add(ppRs);
                                }
                            }

                            return result;
                        }
                );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<ProductViewModel>> Gets(string condition = "", int languageId = 1, DateTime? timeGet = null)
        {
            if (timeGet == null)
            {
                timeGet = DateTime.Now;
            }

            List<ProductViewModel> result = new List<ProductViewModel>();
            string cmd = $@"SELECT * FROM `product` p
                            LEFT JOIN `category` c ON c.id = p.category_id AND c.is_used = 1
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = {languageId}
                            LEFT JOIN `product_price` pp ON p.id = pp.product_id and pp.effectived_date = (SELECT max(ppp.effectived_date) FROM `product_price` ppp 
                                                                                  WHERE ppp.product_id = p.id AND ppp.effectived_date <= '{timeGet.Value.ToString("yyyyMMdd")}'
                                                                                  ORDER BY ppp.effectived_date desc limit 1)
                            WHERE p.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                    (pRs, cRs, plRs, ppRs) =>
                    {
                        var product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                        result.Add(product);

                        if (product.Category == null)
                        {
                            product.Category = cRs;
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;

                            var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                            if (lang == null)
                            {
                                product.Languages.Add(plRs);
                            }
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            product.CurrentUoM = ppRs.UoMId;
                            product.BuyingCurrentPrice = ppRs.BuyingPrice;
                            product.SellingCurrentPrice = ppRs.SellingPrice;
                            product.CurrentWeight = ppRs.Weight;
                            product.CurrentCapacity = ppRs.Capacity;

                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            product.CurrentUoM = product.DefaultUoMId;
                            product.BuyingCurrentPrice = product.DefaultBuyingPrice;
                            product.SellingCurrentPrice = product.DefaultSellingPrice;
                        }

                        return product;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                        (pRs, cRs, plRs, ppRs) =>
                        {
                            var product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                            result.Add(product);

                            if (product.Category == null)
                            {
                                product.Category = cRs;
                            }

                            if (plRs != null)
                            {
                                product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                                product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;

                                var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                                if (lang == null)
                                {
                                    product.Languages.Add(plRs);
                                }
                            }
                            else
                            {
                                product.Name = product.DefaultName;
                                product.Decription = product.DefaultDescription;
                            }

                            if (ppRs != null)
                            {
                                product.CurrentUoM = ppRs.UoMId;
                                product.BuyingCurrentPrice = ppRs.BuyingPrice;
                                product.SellingCurrentPrice = ppRs.SellingPrice;
                                product.CurrentWeight = ppRs.Weight;
                                product.CurrentCapacity = ppRs.Capacity;

                                var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    product.Prices.Add(ppRs);
                                }
                            }
                            else
                            {
                                product.CurrentUoM = product.DefaultUoMId;
                                product.BuyingCurrentPrice = product.DefaultBuyingPrice;
                                product.SellingCurrentPrice = product.DefaultSellingPrice;
                            }

                            return product;
                        }
                    );

                    return result;
                }

            }

        }

        public async Task<IEnumerable<ProductViewModel>> GetsForOrder(string condition = "", int languageId = 1, DateTime? timeGet = null)
        {
            if (timeGet == null)
            {
                timeGet = DateTime.Now;
            }

            List<ProductViewModel> result = new List<ProductViewModel>();
            string cmd = $@"SELECT * FROM `product` p
                            LEFT JOIN `category` c ON c.id = p.category_id AND c.is_used = 1
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = {languageId}
                            LEFT JOIN `product_price` pp ON p.id = pp.product_id and pp.effectived_date = (SELECT max(ppp.effectived_date) FROM `product_price` ppp 
                                                                                  WHERE ppp.product_id = p.id AND ppp.effectived_date <= '{timeGet.Value.ToString("yyyyMMdd")}'
                                                                                  ORDER BY ppp.effectived_date desc limit 1)
                            WHERE p.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                    (pRs, cRs, plRs, ppRs) =>
                    {
                        var product = result.FirstOrDefault(p => p.Id == pRs.Id);
                        if(product == null)
                        {
                            product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                            result.Add(product);
                        }

                        if (product.Category == null)
                        {
                            product.Category = cRs;
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;

                            var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                            if (lang == null)
                            {
                                product.Languages.Add(plRs);
                            }
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            product.CurrentUoM = ppRs.UoMId;
                            product.BuyingCurrentPrice = ppRs.BuyingPrice;
                            product.SellingCurrentPrice = ppRs.SellingPrice;
                            product.CurrentWeight = ppRs.Weight;
                            product.CurrentCapacity = ppRs.Capacity;

                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            product.CurrentUoM = product.DefaultUoMId;
                            product.BuyingCurrentPrice = product.DefaultBuyingPrice;
                            product.SellingCurrentPrice = product.DefaultSellingPrice;
                        }

                        return product;
                    }
                );
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                    (pRs, cRs, plRs, ppRs) =>
                    {
                        var product = result.FirstOrDefault(p => p.Id == pRs.Id);
                        if (product == null)
                        {
                            product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                            result.Add(product);
                        }

                        if (product.Category == null)
                        {
                            product.Category = cRs;
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;

                            var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                            if (lang == null)
                            {
                                product.Languages.Add(plRs);
                            }
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            product.CurrentUoM = ppRs.UoMId;
                            product.BuyingCurrentPrice = ppRs.BuyingPrice;
                            product.SellingCurrentPrice = ppRs.SellingPrice;
                            product.CurrentWeight = ppRs.Weight;
                            product.CurrentCapacity = ppRs.Capacity;

                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            product.CurrentUoM = product.DefaultUoMId;
                            product.BuyingCurrentPrice = product.DefaultBuyingPrice;
                            product.SellingCurrentPrice = product.DefaultSellingPrice;
                        }

                        return product;
                    }
                );
                }

            }

            foreach (var item in result)
            {
                if(item.Prices.Count == 0)
                {
                    item.Prices.Add(new ProductPrice()
                    {
                        ProductId = item.Id,
                        UoMId = item.DefaultUoMId,
                        BuyingPrice = item.DefaultBuyingPrice,
                        SellingPrice = item.DefaultSellingPrice,
                        Weight = 0,
                        Capacity = 0,
                        Id = 1,
                        EffectivedDate = DateTime.Now
                    });
                }
            }

            return result;
        }

        public async Task<IEnumerable<ProductViewModel>> GetsFull(string condition = "")
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            string cmd = $@"SELECT * FROM `product` p
                            LEFT JOIN `category` c ON c.id = p.category_id AND c.is_used = 1
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id
                            LEFT JOIN `product_price` pp ON pp.product_id = p.id
                            WHERE p.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                    (pRs, cRs, plRs, ppRs) =>
                    {
                        var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                        if (product == null)
                        {
                            product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                            result.Add(product);
                        }

                        if (product.Category == null)
                        {
                            product.Category = cRs;
                        }

                        if (plRs != null)
                        {
                            var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                            if (lang == null)
                            {
                                product.Languages.Add(plRs);
                            }
                        }

                        if (ppRs != null)
                        {
                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }

                        return product;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Product, Category, ProductLanguage, ProductPrice, ProductViewModel>(
                        (pRs, cRs, plRs, ppRs) =>
                        {
                            var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                            if (product == null)
                            {
                                product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                                result.Add(product);
                            }

                            if (product.Category == null)
                            {
                                product.Category = cRs;
                            }

                            if (plRs != null)
                            {
                                var lang = product.Languages.FirstOrDefault(l => l.Id == plRs.Id);
                                if (lang == null)
                                {
                                    product.Languages.Add(plRs);
                                }
                            }

                            if (ppRs != null)
                            {
                                var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    product.Prices.Add(ppRs);
                                }
                            }

                            return product;
                        }
                    );

                    return result;
                }

            }

        }

        public async Task<IEnumerable<Product>> GetsOnly()
        {
            return await DALHelper.ExecuteQuery<Product>("SELECT * FROM product WHERE is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<ProductViewModel>> GetsOnlyWithLang(int languageId = 1)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            string cmd = $@"SELECT * FROM `product` p
                            LEFT JOIN `category` c ON c.id = p.category_id AND c.is_used = 1
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = {languageId}
                            WHERE p.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, Category, ProductLanguage, ProductViewModel>(
                    (pRs, cRs, plRs) =>
                    {
                        var product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                        result.Add(product);

                        if (product.Category == null)
                        {
                            product.Category = cRs;
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        return product;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Product, Category, ProductLanguage, ProductViewModel>(
                        (pRs, cRs, plRs) =>
                        {
                            var product = CommonHelper.Mapper<Product, ProductViewModel>(pRs);
                            result.Add(product);

                            if (product.Category == null)
                            {
                                product.Category = cRs;
                            }

                            if (plRs != null)
                            {
                                product.Name = string.IsNullOrEmpty(plRs.Name) ? product.DefaultName : plRs.Name;
                                product.Decription = string.IsNullOrEmpty(plRs.Description) ? product.DefaultDescription : plRs.Description;
                            }
                            else
                            {
                                product.Name = product.DefaultName;
                                product.Decription = product.DefaultDescription;
                            }

                            return product;
                        }
                    );

                    return result;
                }

            }
        }
    }
}
