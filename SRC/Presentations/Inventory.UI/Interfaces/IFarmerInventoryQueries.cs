using Common.Interfaces;
using Inventory.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.UI.Interfaces
{
    public interface IFarmerInventoryQueries : IBaseQueries
    {
        Task<IEnumerable<FarmerProductViewModel>> GetByProductId(int productId, DateTime effect);
    }
}
