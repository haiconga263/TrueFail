using Common.Models;
using QC.UI.Interface;
using QC.UI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QC.Repositories
{
    public class QCStaffRepository : BaseRepository, IQCStaff
    {
        public UserSession LoginSession { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<IEnumerable<IQCStaff>> GetLocationField(string conditions)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<QCStaff>> IQCStaff.GetLocationField(string conditions)
        {
            throw new NotImplementedException();
        }
    }
}
