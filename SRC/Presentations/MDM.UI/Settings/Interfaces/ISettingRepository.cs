using Common.Interfaces;
using MDM.UI.Settings.Models;
using System.Threading.Tasks;

namespace MDM.UI.Settings.Interfaces
{
    public interface ISettingRepository: IBaseRepository
    {
        Task<int> AddAsync(Setting setting);
        Task<int> UpdateAsync(Setting setting);
        Task<int> DeleteAsync(int id);
    }
}
