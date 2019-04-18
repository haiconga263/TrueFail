using Common.Models;
using DAL;
using Dapper;
using GS1.UI.GTINs.Mappings;
using GS1.UI.GTINs.Models;
using GS1.UI.Processes.Interfaces;
using GS1.UI.Processes.Models;
using GS1.UI.Processes.ViewModels;
using GS1.UI.Productions.Models;
using GS1.UI.Productions.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS1.Processes.Queries
{
    public class ProcessQueries : BaseQueries, IProcessQueries
    {
        public async Task<IEnumerable<ProcessInformation>> GetAllAsync(int? partnerId = null)
        {
            string cmd = $@"SELECT * FROM `production_process` pp
	                        LEFT JOIN `production` p ON p.id = pp.production_id
	                        LEFT JOIN `gtin` g ON g.id = p.gtin_id
	                        WHERE pp.`is_deleted` = 0 ";

            if ((partnerId ?? 0) > 0)
                cmd += $" AND pp.`partner_id` = '{partnerId ?? 0}'";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Process, Production, GTIN, ProcessInformation>(
                           (processRs, productionRs, gTINRs) =>
                           {
                               ProcessInformation process = null;
                               if (processRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(processRs);
                                   process = JsonConvert.DeserializeObject<ProcessInformation>(serializedParent);
                               }
                               else process = new ProcessInformation();

                               if (productionRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(productionRs);
                                   process.Production = JsonConvert.DeserializeObject<ProductionInformation>(serializedParent);

                                   if (gTINRs != null)
                                       process.Production.GTIN = gTINRs.ToInformation();
                               }
                               return process;
                           }
                       );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<ProcessInformation> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT * FROM `production_process` pp
	                        LEFT JOIN `production` p ON p.id = pp.production_id
	                        LEFT JOIN `gtin` g ON g.id = p.gtin_id
	                        WHERE pp.`is_deleted` = 0 AND pp.`id` = '{id}' ";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Process, Production, GTIN, ProcessInformation>(
                           (processRs, productionRs, gTINRs) =>
                           {
                               ProcessInformation process = null;
                               if (processRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(processRs);
                                   process = JsonConvert.DeserializeObject<ProcessInformation>(serializedParent);
                               }
                               else process = new ProcessInformation();

                               if (productionRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(productionRs);
                                   process.Production = JsonConvert.DeserializeObject<ProductionInformation>(serializedParent);

                                   if (gTINRs != null)
                                       process.Production.GTIN = gTINRs.ToInformation();
                               }
                               return process;
                           }
                       ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<IEnumerable<ProcessInformation>> GetsAsync(int? partnerId = null)
        {
            string cmd = $@"SELECT * FROM `production_process` pp
	                        LEFT JOIN `production` p ON p.id = pp.production_id
	                        LEFT JOIN `gtin` g ON g.id = p.gtin_id
	                        WHERE pp.`is_deleted` = 0 AND pp.`is_used` = 1 ";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Process, Production, GTIN, ProcessInformation>(
                           (processRs, productionRs, gTINRs) =>
                           {
                               ProcessInformation process = null;
                               if (processRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(processRs);
                                   process = JsonConvert.DeserializeObject<ProcessInformation>(serializedParent);
                               }
                               else process = new ProcessInformation();

                               if (productionRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(productionRs);
                                   process.Production = JsonConvert.DeserializeObject<ProductionInformation>(serializedParent);

                                   if (gTINRs != null)
                                       process.Production.GTIN = gTINRs.ToInformation();
                               }
                               return process;
                           }
                       );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

    }
}
