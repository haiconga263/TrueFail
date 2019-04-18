using Common.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IDistrictRepository : IBaseRepository
    {
        Task<int> Add(District district);
        Task<int> Update(District district);
        Task<int> Delete(District district);
    }
}
