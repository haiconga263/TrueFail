using Common.Interfaces;
using Common.Models;
using GS1.UI.Buffers.Enumerations;
using GS1.UI.SessionBuffers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.SessionBuffers.Interfaces
{
    public interface ISessionBufferService : IBaseService
    {
        Task<IEnumerable<SessionBuffer>> GetByCompanyIdAsync(int partnerId, SessionBufferTypes sessionBufferTypes);
        Task<int> InsertOrUpdateAsync(SessionBuffer model, UserSession session);
    }
}
