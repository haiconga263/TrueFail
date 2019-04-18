using Notifications.UI.Messages.Models;
using Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.UI.Messages.Interfaces
{
    public interface IMessagingRepository : IBaseRepository
    {
        Task<int> AddAsync(Message msg, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> UpdateAsync(Message msg, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteAsync(int id, int modifiedBy, CancellationToken cancellationToken = default(CancellationToken));
    }
}
