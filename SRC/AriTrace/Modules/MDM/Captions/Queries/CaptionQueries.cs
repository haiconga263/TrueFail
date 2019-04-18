using DAL;
using MDM.UI.Captions.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MDM.UI.Captions.ViewModels;
using Newtonsoft.Json;
using MDM.UI.CaptionLanguages.Models;
using MDM.UI.Captions.Models;
using Common.Models;
using System;

namespace MDM.Captions.Queries
{
    public class CaptionQueries : BaseQueries, ICaptionQueries
    {
        public async Task<IEnumerable<UI.Captions.Models.Caption>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `caption` WHERE `is_deleted` = 0";
            return await DALHelper.Query<UI.Captions.Models.Caption>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<CaptionMultipleLanguage> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT c.*, cl.* FROM aritrace.`caption` c
	                        LEFT JOIN aritrace.`caption_language` cl ON c.`id` = cl.`caption_id`
                            WHERE c.`id` = '{id}' AND c.`is_deleted` = 0;";
            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                CaptionMultipleLanguage caption = null;
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<UI.Captions.Models.Caption, UI.CaptionLanguages.Models.CaptionLanguage, CaptionMultipleLanguage>(
                           (captionRs, captionLanguageRs) =>
                           {
                               if (caption == null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(captionRs);
                                   caption = JsonConvert.DeserializeObject<CaptionMultipleLanguage>(serializedParent);
                               }
                               if (captionLanguageRs != null)
                               {
                                   if (caption.Languages == null)
                                       caption.Languages = new List<UI.CaptionLanguages.Models.CaptionLanguage>();
                                   caption.Languages.Add(captionLanguageRs);
                               }
                               return caption;
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

        public async Task<IEnumerable<UI.Captions.Models.Caption>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `caption` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<UI.Captions.Models.Caption>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
