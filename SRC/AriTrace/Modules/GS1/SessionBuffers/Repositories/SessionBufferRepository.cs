using Common.Models;
using DAL;
using GS1.UI.SessionBuffers.Interfaces;
using GS1.UI.SessionBuffers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1.SessionBuffers.Repositories
{
    public class SessionBufferRepository : BaseRepository, ISessionBufferRepository
    {
        public async Task<int> AddAsync(SessionBuffer sessionBuffer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(sessionBuffer);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `session_buffer` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(SessionBuffer sessionBuffer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(sessionBuffer);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
