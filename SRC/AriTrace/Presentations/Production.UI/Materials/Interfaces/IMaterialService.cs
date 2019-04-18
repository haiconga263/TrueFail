using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Production.UI.Materials.Interfaces
{
    public interface IMaterialService : IBaseService
    {
        Task<string> GenerateCodeAsync(int? partnerId);
    }
}
