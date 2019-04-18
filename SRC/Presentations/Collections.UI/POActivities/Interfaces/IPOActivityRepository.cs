using Common.Interfaces;
using Collections.UI.POActivities.Models;
using System.Threading.Tasks;

namespace Collections.UI.POActivities.Interfaces
{
    public interface IPOActivityRepository: IBaseRepository
    {
        Task<int> AddAsync(POActivity poActivity);
    }
}
