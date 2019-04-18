using Common.Interfaces;
using MDM.UI.UoMs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.UoMs.Interfaces
{
    public interface IUoMQueries : IBaseQueries
    {
        Task<UoM> Get(int uomId);
        Task<IEnumerable<UoM>> Gets(string condition = "");
    }
}
