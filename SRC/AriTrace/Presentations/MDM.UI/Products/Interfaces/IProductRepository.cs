using Common.Interfaces;
using MDM.UI.Products.Models;
using System.Threading.Tasks;

namespace MDM.UI.Products.Interfaces
{
    public interface IProductRepository: IBaseRepository
    {
        Task<int> AddAsync(Product product);
        Task<int> UpdateAsync(Product product);
        Task<int> DeleteAsync(int id);
    }
}
