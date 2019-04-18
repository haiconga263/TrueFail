using Common.Models;
using DAL;
using GS1.UI.Buffers.Enumerations;
using GS1.UI.SessionBuffers.Interfaces;
using GS1.UI.SessionBuffers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS1.SessionBuffers.Queries
{
    public class SessionBufferQueries : BaseQueries, ISessionBufferQueries
    {
        public async Task<IEnumerable<SessionBuffer>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `session_buffer` WHERE `is_deleted` = 0";
            return await DALHelper.Query<SessionBuffer>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<SessionBuffer>> GetByCompanyIdAsync(int partnerId, SessionBufferTypes sessionBufferTypes)
        {
            string cmd = $"SELECT * FROM `session_buffer` WHERE `is_deleted` = 0 AND `partner_id` = '{partnerId}' AND `type` = '{sessionBufferTypes}'";
            return await DALHelper.Query<SessionBuffer>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<SessionBuffer> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<SessionBuffer>($"SELECT * FROM `session_buffer` WHERE `is_deleted` = 0 AND `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<SessionBuffer>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `session_buffer` WHERE `is_deleted` = 0 ";
            return await DALHelper.Query<SessionBuffer>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
