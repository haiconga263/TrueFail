using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Storages.Interfaces
{
    public interface IStorageRepository
    {
        Task<int> AddAsync(string key);
    }
}
