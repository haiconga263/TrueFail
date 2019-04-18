using Common.Interfaces;
using MDM.UI.Geographical.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IDistrictQueries : IBaseQueries
    {
        Task<DistrictViewModel> Get(int districtId);
        Task<IEnumerable<DistrictViewModel>> Gets(string condition = "");
        Task<IEnumerable<DistrictCommon>> GetCommons();
    }
}
