using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Accounts.Models;
using Newtonsoft.Json;
using Production.UI.CultureFields.Models;
using Production.UI.MaterialHistories.Interfaces;
using Production.UI.MaterialHistories.Models;
using Production.UI.MaterialHistories.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.MaterialHistories.Queries
{
    public class MaterialHistoryQueries : BaseQueries, IMaterialHistoryQueries
    {
        public async Task<MaterialHistoryInfomation> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT mh.*, cf.*, ua1.*, ua2.* FROM aritrace.`material_history` mh
                                LEFT JOIN aritrace.`culture_field` cf ON mh.`culture_field_id` = cf.`id`
                                LEFT JOIN aritrace.`user_account` ua1 ON mh.`created_by` = ua1.`id`
                                LEFT JOIN aritrace.`user_account` ua2 ON mh.`deleted_by` = ua2.`id`
                                WHERE mh.`id` = '{id}'";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<MaterialHistory, CultureField, Account, Account, MaterialHistoryInfomation>(
                           (materialHistoryRs, cultureFieldRs, userCreatedRs, userDeletedRs) =>
                           {
                               MaterialHistoryInfomation info = null;
                               if (info == null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(materialHistoryRs);
                                   info = JsonConvert.DeserializeObject<MaterialHistoryInfomation>(serializedParent);
                               }

                               if (cultureFieldRs != null) info.CultureField = cultureFieldRs;
                               if (userCreatedRs != null) info.UserCreated = userCreatedRs;
                               if (userDeletedRs != null) info.UserDeleted = userDeletedRs;

                               return info;
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

        public async Task<IEnumerable<MaterialHistoryInfomation>> GetAllAsync(int materialId)
        {
            string cmd = $@"SELECT mh.*, cf.*, ua1.*, ua2.* FROM aritrace.`material_history` mh
                                LEFT JOIN aritrace.`culture_field` cf ON mh.`culture_field_id` = cf.`id`
                                LEFT JOIN aritrace.`user_account` ua1 ON mh.`created_by` = ua1.`id`
                                LEFT JOIN aritrace.`user_account` ua2 ON mh.`deleted_by` = ua2.`id`
                                WHERE mh.`material_id` = '{materialId}'";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<MaterialHistory, CultureField, Account, Account, MaterialHistoryInfomation>(
                           (materialHistoryRs, cultureFieldRs, userCreatedRs, userDeletedRs) =>
                           {
                               MaterialHistoryInfomation info = null;
                               if (info == null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(materialHistoryRs);
                                   info = JsonConvert.DeserializeObject<MaterialHistoryInfomation>(serializedParent);
                               }

                               if (cultureFieldRs != null) info.CultureField = cultureFieldRs;
                               if (userCreatedRs != null) info.UserCreated = userCreatedRs;
                               if (userDeletedRs != null) info.UserDeleted = userDeletedRs;

                               return info;
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
