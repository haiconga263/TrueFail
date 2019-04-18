using AriSystem.UI.Storages.Enumerations;
using AriSystem.UI.Storages.Interfaces;
using Production.UI.Materials.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Services;

namespace Production.Materials.Servicers
{
    public class MaterialService : BaseService, IMaterialService
    {
        private readonly IStorageQueries _storageQueries = null;

        public MaterialService(IStorageQueries storageQueries)
        {
            this._storageQueries = storageQueries;
        }
        
        public async Task<string> GenerateCodeAsync(int? partnerId)
        {
            var num = await _storageQueries.GetValueAndIncOneAsync(StorageKeys.MaterialCode);

            return $"MTC{num.ToString().PadLeft(5, '0')}";
        }
    }
}
