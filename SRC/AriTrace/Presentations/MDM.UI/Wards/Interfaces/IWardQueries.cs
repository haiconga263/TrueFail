using Common.Interfaces;
using MDM.UI.Wards.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Wards.Interfaces
{
    public interface IWardQueries : IBaseQueries
    {
        Task<Ward> GetByIdAsync(int id);

        Task<IEnumerable<Ward>> GetsAsync();
        Task<IEnumerable<Ward>> GetAllAsync();
    }
}
