using Common.Models;
using DAL;
using GS1.UI.Processes.Interfaces;
using GS1.UI.Processes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1.Processes.Repositories
{
    public class ProcessRepository : BaseRepository, IProcessRepository
    {
        public async Task<int> AddAsync(Process process)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(process);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `production_process` WHERE `id` = {id};";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Process process)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(process);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
