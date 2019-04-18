using Common.Interfaces;
using MDM.UI.Retailers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Retailers.Interfaces
{
    public interface IRetailerRepository : IBaseRepository
    {
        Task<int> Add(Retailer retailer);
        Task<int> Update(Retailer retailer);
        Task<int> Delete(Retailer retailer);

        Task<int> AddLocation(RetailerLocation retailerLocation);
        Task<int> UpdateLocation(RetailerLocation retailerLocation);
        Task<int> DeleteLocation(RetailerLocation retailerLocation);
    }
}
