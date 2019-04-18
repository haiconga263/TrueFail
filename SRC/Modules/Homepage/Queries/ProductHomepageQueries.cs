using Common.Models;
using Homepage.UI.Interfaces;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Dapper;
using DAL;
using MDM.UI.Categories.Models;
using MDM.UI.Products.Models;

namespace Homepage.Queries
{
    public class ProductHomepageQueries : BaseQueries, IProductHomepageQueries
    {
        private string categoryQuery = $@"(SELECT c.`id`, c.`code`, 
		                                    coalesce(cpl.`caption`, c.`default_name`) as 'default_name', 
                                            c.`caption_name_id`, c.`is_used`, 
                                            c.`parent_id`, c.`is_deleted`, c.`created_date`, 
                                            c.`created_by`, c.`modified_date`, c.`modified_by`
	                                    FROM category c
	                                    LEFT JOIN `language` l ON l.code = 'en'
	                                    LEFT JOIN `caption_language` cpl ON cpl.language_id = l.id AND cpl.caption_id = c.caption_name_id
	                                    WHERE c.`is_deleted` = 0 AND c.`is_used` = 1 )";

        public async Task<IEnumerable<ProductHomepageViewModel>> GetProductAsync(string condition = "", int pageIndex = 2, int pageSize = 10, string lang = "vi")
        {
            List<ProductHomepageViewModel> result = new List<ProductHomepageViewModel>();
            string cmd = $@"SELECT p.*, pl.*, pp.*, cte.* FROM `product` as p 
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = l.id
                            LEFT JOIN `product_price` pp ON pp.product_id = p.id
                            LEFT JOIN {categoryQuery} cte ON cte.id = p.category_id
                            WHERE p.is_deleted = 0 AND p.is_used = '1'";
            if (!string.IsNullOrWhiteSpace(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                    (pRs, plRs, ppRs, cteRs) =>
                    {
                        var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                        if (product == null)
                        {
                            product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                            result.Add(product);
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            product.CurrentUoM = ppRs.UoMId;
                            product.SellingCurrentPrice = ppRs.SellingPrice;

                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            product.CurrentUoM = product.DefaultUoMId;
                            product.SellingCurrentPrice = product.DefaultSellingPrice;
                        }

                        if (cteRs != null)
                        {
                            product.Category = cteRs;
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
                    rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                        (pRs, plRs, ppRs, cteRs) =>
                        {
                            var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                            if (product == null)
                            {
                                product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                                result.Add(product);
                            }

                            if (plRs != null)
                            {
                                product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                                product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                            }
                            else
                            {
                                product.Name = product.DefaultName;
                                product.Decription = product.DefaultDescription;
                            }

                            if (ppRs != null)
                            {
                                product.CurrentUoM = ppRs.UoMId;
                                product.SellingCurrentPrice = ppRs.SellingPrice;

                                var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    product.Prices.Add(ppRs);
                                }
                            }
                            else
                            {
                                product.CurrentUoM = product.DefaultUoMId;
                                product.SellingCurrentPrice = product.DefaultSellingPrice;
                            }

                            if (cteRs != null)
                            {
                                product.Category = cteRs;
                            }

                            return product;
                        }
                    );
                    return result;
                }

            }
        }

        public async Task<ProductHomepageViewModel> GetProductDetailOfHomepageAsync(int productId, string lang = "vi")
        {
            ProductHomepageViewModel result = null;
            string cmd = $@"SELECT p.*, pl.*, pp.*, cte.*  FROM `product` p
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = l.id
                            LEFT JOIN `product_price` pp ON pp.product_id = p.id
                            LEFT JOIN {categoryQuery} cte ON cte.id = p.category_id
                            WHERE p.id = {productId} and p.is_deleted = 0 AND p.is_used = '1'";

            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                    (pRs, plRs, ppRs, cteRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                        }

                        if (plRs != null)
                        {
                            result.Name = string.IsNullOrWhiteSpace(plRs.Name) ? result.DefaultName : plRs.Name;
                            result.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? result.DefaultDescription : plRs.Description;
                        }
                        else
                        {
                            result.Name = result.DefaultName;
                            result.Decription = result.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            result.CurrentUoM = ppRs.UoMId;
                            result.SellingCurrentPrice = ppRs.SellingPrice;

                            var price = result.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                result.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            result.CurrentUoM = result.DefaultUoMId;
                            result.SellingCurrentPrice = result.DefaultSellingPrice;
                        }
                        if (cteRs != null)
                        {
                            result.Category = cteRs;
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
                    rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                        (pRs, plRs, ppRs, cteRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                            }

                            if (plRs != null)
                            {
                                result.Name = string.IsNullOrWhiteSpace(plRs.Name) ? result.DefaultName : plRs.Name;
                                result.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? result.DefaultDescription : plRs.Description;
                            }
                            else
                            {
                                result.Name = result.DefaultName;
                                result.Decription = result.DefaultDescription;
                            }

                            if (ppRs != null)
                            {
                                result.CurrentUoM = ppRs.UoMId;
                                result.SellingCurrentPrice = ppRs.SellingPrice;

                                var price = result.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    result.Prices.Add(ppRs);
                                }
                            }
                            else
                            {
                                result.CurrentUoM = result.DefaultUoMId;
                                result.SellingCurrentPrice = result.DefaultSellingPrice;
                            }
                            if (cteRs != null)
                            {
                                result.Category = cteRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<ProductHomepageViewModel>> GetProductRelatedAsync(int categoryId, string lang = "vi")
        {
            List<ProductHomepageViewModel> result = new List<ProductHomepageViewModel>();
            string cmd = $@"SELECT p.*, pl.*, pp.*, cte.* FROM category cte
                            LEFT JOIN `product` p ON cte.id = p.category_id AND p.is_deleted = 0 AND p.is_used = '1'
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = l.id
                            LEFT JOIN `product_price` pp ON pp.product_id = p.id
                            WHERE cte.id = '{categoryId}'";

            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                    (pRs, plRs, ppRs, cteRs) =>
                    {
                        var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                        if (product == null)
                        {
                            product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                            result.Add(product);
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            product.CurrentUoM = ppRs.UoMId;
                            product.SellingCurrentPrice = ppRs.SellingPrice;

                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            product.CurrentUoM = product.DefaultUoMId;
                            product.SellingCurrentPrice = product.DefaultSellingPrice;
                        }

                        if (cteRs != null)
                        {
                            product.Category = cteRs;
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
                    rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                        (pRs, plRs, ppRs, cteRs) =>
                        {
                            var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                            if (product == null)
                            {
                                product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                                result.Add(product);
                            }

                            if (plRs != null)
                            {
                                product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                                product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                            }
                            else
                            {
                                product.Name = product.DefaultName;
                                product.Decription = product.DefaultDescription;
                            }

                            if (ppRs != null)
                            {
                                product.CurrentUoM = ppRs.UoMId;
                                product.SellingCurrentPrice = ppRs.SellingPrice;

                                var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    product.Prices.Add(ppRs);
                                }
                            }
                            else
                            {
                                product.CurrentUoM = product.DefaultUoMId;
                                product.SellingCurrentPrice = product.DefaultSellingPrice;
                            }

                            if (cteRs != null)
                            {
                                product.Category = cteRs;
                            }

                            return product;
                        }
                    );
                    return result;
                }

            }
        }

        public async Task<IEnumerable<ProductHomepageViewModel>> GetProductOutstandingOfHomepage(string lang = "vi")
        {
            List<ProductHomepageViewModel> result = new List<ProductHomepageViewModel>();
            string cmd = $@"SELECT p.*, pl.*, pp.*, cte.* FROM `product` as p 
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = l.id
                            LEFT JOIN `product_price` pp ON pp.product_id = p.id
                            LEFT JOIN {categoryQuery} cte ON cte.id = p.category_id
                            WHERE p.is_deleted = 0 AND p.is_used = '1' ";

            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                    (pRs, plRs, ppRs, cteRs) =>
                    {
                        var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                        if (product == null)
                        {
                            product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                            result.Add(product);
                        }

                        if (plRs != null)
                        {
                            product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                            product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                        }
                        else
                        {
                            product.Name = product.DefaultName;
                            product.Decription = product.DefaultDescription;
                        }

                        if (ppRs != null)
                        {
                            product.CurrentUoM = ppRs.UoMId;
                            product.SellingCurrentPrice = ppRs.SellingPrice;

                            var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                            if (price == null)
                            {
                                product.Prices.Add(ppRs);
                            }
                        }
                        else
                        {
                            product.CurrentUoM = product.DefaultUoMId;
                            product.SellingCurrentPrice = product.DefaultSellingPrice;
                        }

                        if (cteRs != null)
                        {
                            product.Category = cteRs;
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
                    rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                        (pRs, plRs, ppRs, cteRs) =>
                        {
                            var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                            if (product == null)
                            {
                                product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                                result.Add(product);
                            }

                            if (plRs != null)
                            {
                                product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                                product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                            }
                            else
                            {
                                product.Name = product.DefaultName;
                                product.Decription = product.DefaultDescription;
                            }

                            if (ppRs != null)
                            {
                                product.CurrentUoM = ppRs.UoMId;
                                product.SellingCurrentPrice = ppRs.SellingPrice;

                                var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    product.Prices.Add(ppRs);
                                }
                            }
                            else
                            {
                                product.CurrentUoM = product.DefaultUoMId;
                                product.SellingCurrentPrice = product.DefaultSellingPrice;
                            }

                            if (cteRs != null)
                            {
                                product.Category = cteRs;
                            }

                            return product;
                        }
                    );
                    return result;
                }
            }
        }

        public async Task<IEnumerable<ProductHomepageViewModel>> GetProductByCategory(int categoryId, string lang = "vi")
        {
            List<ProductHomepageViewModel> result = new List<ProductHomepageViewModel>();
            string cmd = $@"SELECT p.*, pl.*, pp.*, cte.* FROM category cte
                            LEFT JOIN `product` p ON cte.id = p.category_id AND p.is_deleted = 0 AND p.is_used = '1'
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `product_language` pl ON p.id = pl.product_id AND pl.language_id = l.id
                            LEFT JOIN `product_price` pp ON pp.product_id = p.id
                            WHERE cte.id = '{categoryId}'";

            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                    (pRs, plRs, ppRs, cteRs) =>
                    {
                        var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                        if (product == null && pRs != null && pRs.Id > 0)
                        {
                            product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                            result.Add(product);
                        }

                        if (product != null)
                        {
                            if (plRs != null)
                            {
                                product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                                product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                            }
                            else
                            {
                                product.Name = product.DefaultName;
                                product.Decription = product.DefaultDescription;
                            }

                            if (ppRs != null)
                            {
                                product.CurrentUoM = ppRs.UoMId;
                                product.SellingCurrentPrice = ppRs.SellingPrice;

                                var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                if (price == null)
                                {
                                    product.Prices.Add(ppRs);
                                }
                            }
                            else
                            {
                                product.CurrentUoM = product.DefaultUoMId;
                                product.SellingCurrentPrice = product.DefaultSellingPrice;
                            }

                            if (cteRs != null)
                            {
                                product.Category = cteRs;
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
                    rd.Read<Product, ProductLanguage, ProductPrice, Category, ProductHomepageViewModel>(
                        (pRs, plRs, ppRs, cteRs) =>
                        {
                            var product = result.FirstOrDefault(p => p.Id == pRs.Id);

                            if (product == null && pRs != null && pRs.Id > 0)
                            {
                                product = CommonHelper.Mapper<Product, ProductHomepageViewModel>(pRs);
                                result.Add(product);
                            }

                            if (product!= null)
                            {
                                if (plRs != null)
                                {
                                    product.Name = string.IsNullOrWhiteSpace(plRs.Name) ? product.DefaultName : plRs.Name;
                                    product.Decription = string.IsNullOrWhiteSpace(plRs.Description) ? product.DefaultDescription : plRs.Description;
                                }
                                else
                                {
                                    product.Name = product.DefaultName;
                                    product.Decription = product.DefaultDescription;
                                }

                                if (ppRs != null)
                                {
                                    product.CurrentUoM = ppRs.UoMId;
                                    product.SellingCurrentPrice = ppRs.SellingPrice;

                                    var price = product.Prices.FirstOrDefault(l => l.Id == ppRs.Id);
                                    if (price == null)
                                    {
                                        product.Prices.Add(ppRs);
                                    }
                                }
                                else
                                {
                                    product.CurrentUoM = product.DefaultUoMId;
                                    product.SellingCurrentPrice = product.DefaultSellingPrice;
                                }

                                if (cteRs != null)
                                {
                                    product.Category = cteRs;
                                }
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
