using Common.Interfaces;
using QC.UI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QC.UI.Interface
{
    public interface IQCStaff : IBaseQueries
    {
        Task<IEnumerable<QCStaff>> GetLocationField(string conditions);
    }
}
