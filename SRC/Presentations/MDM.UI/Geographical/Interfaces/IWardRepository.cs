using Common.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface IWardRepository : IBaseRepository
    {
        Task<int> Add(Ward ward);
        Task<int> Update(Ward ward);
        Task<int> Delete(Ward ward);
    }
}
