using Common.Interfaces;
using Distributions.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.UI.Interfaces
{
    public interface IRouterQueries: IBaseQueries
    {
        Task<IEnumerable<RouterViewModel>> Gets(string conditions);
        Task<IEnumerable<RouterViewModel>> Gets(int distributionId);
        Task<RouterViewModel> Get(int id);
    }
}
