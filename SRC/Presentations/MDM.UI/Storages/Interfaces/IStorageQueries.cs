using Common.Interfaces;
using MDM.UI.Storages.Models;
using System.Threading.Tasks;

namespace MDM.UI.Storages.Interfaces
{
    public interface IStorageQueries : IBaseQueries
    {
        Task<Storage> GetValueAndIncOneAsync(string key);
        Task<string> GenarateCodeAsync(string key);
    }
}
