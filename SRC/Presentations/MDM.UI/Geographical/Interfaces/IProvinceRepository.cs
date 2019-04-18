using Common.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IProvinceRepository : IBaseRepository
    {
        Task<int> Add(Province province);
        Task<int> Update(Province province);
        Task<int> Delete(Province province);
    }
}
