using Common.Models;
using DAL;
using MDM.UI.Vehicles.Interfaces;
using MDM.UI.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Vehicles.Repositories
{
    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
        public async Task<int> Add(Vehicle vehicle)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(vehicle);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Vehicle vehicle)
        {
            var cmd = $@"UPDATE `vehicle`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {vehicle.ModifiedBy},
                         `modified_date` = '{vehicle.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {vehicle.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Vehicle vehicle)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(vehicle);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
