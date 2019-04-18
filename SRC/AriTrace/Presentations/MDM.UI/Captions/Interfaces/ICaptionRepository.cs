using Common.Interfaces;
using MDM.UI.Captions.Models;
using System.Threading.Tasks;

namespace MDM.UI.Captions.Interfaces
{
    public interface ICaptionRepository: IBaseRepository
    {
        Task<int> AddAsync(Caption caption);
        Task<int> UpdateAsync(Caption caption);
        Task<int> DeleteAsync(int id);
    }
}
