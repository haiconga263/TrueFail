using Common.Models;
using DAL;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS1.GTINs.Queries
{
    public class GTINQueries : BaseQueries, IGTINQueries
    {
        public async Task<IEnumerable<GTIN>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `gtin`";
            return await DALHelper.Query<GTIN>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<GTIN> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<GTIN>($"SELECT * FROM `gtin` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<GTIN>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `gtin` WHERE `is_used` = 1";
            return await DALHelper.Query<GTIN>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<GTIN>> GetByCompanyCodeAsync(int gS1Code)
        {
            string cmd = $"SELECT * FROM `gtin` WHERE `is_used` = 1 AND `company_code` = '{gS1Code}'";
            return await DALHelper.Query<GTIN>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
