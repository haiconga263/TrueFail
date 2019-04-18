using Common.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IRegionRepository : IBaseRepository
    {
        Task<int> Add(Region region);
        Task<int> Update(Region region);
        Task<int> Delete(Region region);
    }
}
