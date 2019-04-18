using Common.Models;
using DAL;
using Distributions.UI.Interfaces;
using Distributions.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.Repositories
{
    public class TripRepository : BaseRepository, ITripRepository
    {
        public async Task<int> Create(Trip trip)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(trip);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> CreateTripAudit(TripAudit audit)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(audit);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Trip trip)
        {
            var cmd = $@"UPDATE `trip`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {trip.ModifiedBy},
                         `modified_date` = '{trip.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {trip.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Trip trip)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(trip);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
