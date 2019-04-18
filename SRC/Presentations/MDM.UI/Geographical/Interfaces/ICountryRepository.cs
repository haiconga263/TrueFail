using Common.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface ICountryRepository : IBaseRepository
    {
        Task<int> Add(Country country);
        Task<int> Update(Country country);
        Task<int> Delete(Country country);
    }
}
