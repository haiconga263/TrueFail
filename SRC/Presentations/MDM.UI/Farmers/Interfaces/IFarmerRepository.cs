using Common.Interfaces;
using MDM.UI.Farmers.Models;
using MDM.UI.Geographical.Models;
using System.Threading.Tasks;

namespace MDM.UI.Farmers.Interfaces
{
    public interface IFarmerRepository : IBaseRepository
    {
        Task<int> Add(Farmer farmer);
        Task<int> Update(Farmer farmer);
        Task<int> Delete(Farmer farmer);
    }
}
