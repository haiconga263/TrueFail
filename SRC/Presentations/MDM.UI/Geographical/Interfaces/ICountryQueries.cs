using Common.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Geographical.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface ICountryQueries : IBaseQueries
    {
        Task<Country> Get(int countryId);
        Task<IEnumerable<Country>> Gets(string condition = "");
        Task<IEnumerable<CountryCommon>> GetCommons();
    }
}
