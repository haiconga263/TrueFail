using Abivin.Integration.UI.ViewModels;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abivin.Integration.UI.Interfaces
{
    public interface IAbivinCustomerQueries : IBaseQueries
    {
        Task<CustomerViewModel> Get(string code);
        Task<IEnumerable<CustomerViewModel>> Gets(string condition = "");
    }
}
