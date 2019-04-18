using Common.Models;
using DAL;
using QC.UI.Interface;
using QC.UI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QC.Queries
{
    public class QCStaffQueries : BaseQueries, IQCStaff
    {
        public Task<IEnumerable<QCStaff>> GetLocationField(string conditions)
        {
            return DALHelper.ExecuteQuery<QCStaff>("SELECT * FROM qc_staff", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
