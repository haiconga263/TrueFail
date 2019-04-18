using Common.Interfaces;
using GS1.UI.SessionBuffers.Models;
using System.Threading.Tasks;

namespace GS1.UI.SessionBuffers.Interfaces
{
    public interface ISessionBufferRepository: IBaseRepository
    {
        Task<int> AddAsync(SessionBuffer sessionBuffer);
        Task<int> UpdateAsync(SessionBuffer sessionBuffer);
        Task<int> DeleteAsync(int id);
    }
}
