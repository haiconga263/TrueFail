using Common.Interfaces;
using MDM.UI.Geographical.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IWardQueries : IBaseQueries
    {
        Task<WardViewModel> Get(int wardId);
        Task<IEnumerable<WardViewModel>> Gets(string condition = "");
        Task<IEnumerable<WardCommon>> GetCommons();
    }
}
