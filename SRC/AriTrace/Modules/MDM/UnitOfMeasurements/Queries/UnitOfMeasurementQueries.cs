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

namespace MDM.UnitOfMeasurements.Queries
{
    public class UnitOfMeasurementQueries : BaseQueries, IUnitOfMeasurementQueries
    {
        public async Task<IEnumerable<UOM>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `uom`";
            return await DALHelper.Query<UOM>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<UOM> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<UOM>($"SELECT * FROM `uom` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<UOM>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `uom` WHERE `is_used` = 1";
            return await DALHelper.Query<UOM>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
