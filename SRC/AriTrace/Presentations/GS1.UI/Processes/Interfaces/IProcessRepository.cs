using Common.Interfaces;
using GS1.UI.Processes.Models;
using System.Threading.Tasks;

namespace GS1.UI.Processes.Interfaces
{
    public interface IProcessRepository: IBaseRepository
    {
        Task<int> AddAsync(Process process);
        Task<int> UpdateAsync(Process process);
        Task<int> DeleteAsync(int id);
    }
}
