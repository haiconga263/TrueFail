using Common.Models;
using DAL;
using MDM.UI.UoMs.Interfaces;
using MDM.UI.UoMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UoMs.Queries
{
    public class UoMQueries : BaseQueries, IUoMQueries
    {
        public async Task<UoM> Get(int uomId)
        {
            return (await DALHelper.Query<UoM>($"SELECT * FROM `uom` WHERE `id` = {uomId}  AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<UoM>> Gets(string condition = "")
        {
            string cmd = $"SELECT * FROM `uom` WHERE is_deleted = 0";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            return await DALHelper.Query<UoM>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
