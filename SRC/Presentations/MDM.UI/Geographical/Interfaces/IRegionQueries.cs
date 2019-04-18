using Common.Interfaces;
using MDM.UI.Geographical.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IRegionQueries : IBaseQueries
    {
        Task<RegionViewModel> Get(int regionId);
        Task<IEnumerable<RegionViewModel>> Gets(string condition = "");
        Task<IEnumerable<RegionCommon>> GetCommons();
    }
}
