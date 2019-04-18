using Common.Interfaces;
using Distributions.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.UI.Interfaces
{
    public interface IRouterRepository : IBaseRepository
    {
        Task<int> Create(Router router);
        Task<int> Update(Router router);
        Task<int> Delete(Router router);
    }
}
