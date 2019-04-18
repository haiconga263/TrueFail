using Common.Interfaces;
using GS1.UI.GTINs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AriSystem.UI.Storages.Interfaces
{
    public interface IStorageQueries : IBaseQueries
    {
        Task<long> GetValueAsync(string key);
        Task<long> GetValueAndIncOneAsync(string key);
    }
}
