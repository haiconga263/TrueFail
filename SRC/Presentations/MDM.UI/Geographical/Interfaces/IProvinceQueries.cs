using Common.Interfaces;
using MDM.UI.Geographical.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IProvinceQueries : IBaseQueries
    {
        Task<ProvinceViewModel> Get(int provinceId);
        Task<IEnumerable<ProvinceViewModel>> Gets(string condition = "");
        Task<IEnumerable<ProvinceCommon>> GetCommons();
    }
}
