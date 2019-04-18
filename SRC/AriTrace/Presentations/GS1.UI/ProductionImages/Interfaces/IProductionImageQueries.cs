using Common.Interfaces;
using GS1.UI.ProductionImages.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.ProductionImages.Interfaces
{
    public interface IProductionImageQueries : IBaseQueries
    {
        Task<ProductionImage> GetByIdAsync(int id);

        Task<IEnumerable<ProductionImage>> GetsAsync();
        Task<IEnumerable<ProductionImage>> GetAllAsync();
    }
}
