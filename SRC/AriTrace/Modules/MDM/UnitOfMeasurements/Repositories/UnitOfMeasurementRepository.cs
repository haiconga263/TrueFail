using Common.Models;
using DAL;
using MDM.UI.UnitOfMeasurements.Interfaces;
using MDM.UI.UnitOfMeasurements.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UnitOfMeasurements.Repositories
{
    public class UnitOdMeasurementRepository : BaseRepository, IUnitOfMeasurementRepository
    {
        public async Task<int> AddAsync(UOM uom)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(uom);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `uom` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(UOM uom)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(uom);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
