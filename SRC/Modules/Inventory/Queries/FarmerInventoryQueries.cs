using Common.Models;
using DAL;
using Inventory.UI.Interfaces;
using Inventory.UI.ViewModels;
using MDM.UI.Farmers.Models;
using MDM.UI.Products.Models;
using MDM.UI.UoMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Queries
{
    public class FarmerInventoryQueries : BaseQueries, IFarmerInventoryQueries
    {
        public async Task<IEnumerable<FarmerProductViewModel>> GetByProductId(int productId, DateTime effect)
        {
            //MockData : Farmer with all uom
            List<FarmerProductViewModel> result = new List<FarmerProductViewModel>();
            var uoms = await DALHelper.ExecuteQuery<UoM>("SELECT * FROM uom");
            var farmers = await DALHelper.ExecuteQuery<Farmer>("SELECT * FROM farmer");
            var product = (await DALHelper.ExecuteQuery<Product>($"SELECT * FROM product WHERE Id = {productId}")).FirstOrDefault();
            foreach (var uom in uoms)
            {
                foreach (var farmer in farmers)
                {
                    result.Add(new FarmerProductViewModel()
                    {
                        EffectivedDate = effect,
                        FarmerId = farmer.Id,
                        Farmer = farmer,
                        ProductId = productId,
                        UoMId = uom.Id,
                        Product = product,
                        Quantity = 1000000, //Dump data: 1 milion
                        UoM = uom
                    });
                }
            }
            return result;
            // End MockData
        }
    }
}
