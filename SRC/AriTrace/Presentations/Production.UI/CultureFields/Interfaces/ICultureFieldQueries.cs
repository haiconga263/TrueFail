using Common.Interfaces;
using Production.UI.CultureFields.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Production.UI.CultureFields.Interfaces
{
    public interface ICultureFieldQueries : IBaseQueries
    {
        Task<CultureField> GetByIdAsync(int id);
        Task<IEnumerable<CultureField>> GetsAsync();
        Task<IEnumerable<CultureField>> GetAllAsync();
    }
}
