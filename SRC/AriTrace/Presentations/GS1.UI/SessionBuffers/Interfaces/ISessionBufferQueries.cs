using Common.Interfaces;
using GS1.UI.Buffers.Enumerations;
using GS1.UI.SessionBuffers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS1.UI.SessionBuffers.Interfaces
{
    public interface ISessionBufferQueries : IBaseQueries
    {
        Task<SessionBuffer> GetByIdAsync(int id);
        Task<IEnumerable<SessionBuffer>> GetsAsync();
        Task<IEnumerable<SessionBuffer>> GetAllAsync();
        Task<IEnumerable<SessionBuffer>> GetByCompanyIdAsync(int partnerId, SessionBufferTypes sessionBufferTypes);
    }
}
