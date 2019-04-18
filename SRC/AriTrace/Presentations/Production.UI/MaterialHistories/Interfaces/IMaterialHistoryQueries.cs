using Common.Interfaces;
using Production.UI.MaterialHistories.Models;
using Production.UI.MaterialHistories.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Production.UI.MaterialHistories.Interfaces
{
    public interface IMaterialHistoryQueries : IBaseQueries
    {
        Task<MaterialHistoryInfomation> GetByIdAsync(int id);
        Task<IEnumerable<MaterialHistoryInfomation>> GetAllAsync(int materialId);
    }
}
