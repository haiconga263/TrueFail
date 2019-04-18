using AriSystem.UI.Storages.Enumerations;
using AriSystem.UI.Storages.Interfaces;
using Common.Models;
using GS1.UI.Processes.Interfaces;
using GS1.UI.Processes.Models;
using GS1.UI.Processes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Services;

namespace GS1.Processes.Services
{
    public class ProcessService : BaseService, IProcessService
    {
        private readonly IProcessQueries _processQueries = null;
        private readonly IProcessRepository _processRepository = null;
        private readonly IStorageQueries _storageQueries = null;

        public ProcessService(IProcessQueries processQueries, IProcessRepository processRepository,
            IStorageQueries storageQueries)
        {
            this._processQueries = processQueries;
            this._processRepository = processRepository;
            this._storageQueries = storageQueries;
        }


        public async Task<ProcessInformation> NewProcessAsync(int partnerId, UserSession session)
        {
            var process = new Process();
            process.Code = await GenerateTraceCodeAsync();
            process.PartnerId = partnerId;
            process.IsUsed = true;
            process.IsNew = true;
            process.ModifiedDate = DateTime.Now;
            process.ModifiedBy = session.Id;
            process.CreatedDate = DateTime.Now;
            process.CreatedBy = session.Id;

            var id = await _processRepository.AddAsync(process);

            return await _processQueries.GetByIdAsync(id);
        }

        public async Task<string> GenerateTraceCodeAsync()
        {
            var num = await _storageQueries.GetValueAndIncOneAsync(StorageKeys.TraceCode);

            return $"{num.ToString().PadLeft(10, '0')}";
        }
    }
}
