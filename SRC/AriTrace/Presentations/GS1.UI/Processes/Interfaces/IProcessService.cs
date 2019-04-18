using Common.Interfaces;
using Common.Models;
using GS1.UI.Processes.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.Processes.Interfaces
{
    public interface IProcessService : IBaseService
    {
        Task<ProcessInformation> NewProcessAsync(int partnerId, UserSession session);
        Task<string> GenerateTraceCodeAsync();
    }
}
