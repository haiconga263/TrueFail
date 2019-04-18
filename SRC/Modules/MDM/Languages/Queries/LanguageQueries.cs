using Common.Helpers;
using Common.Models;
using Common.ViewModels;
using DAL;
using Dapper;
using MDM.UI.Languages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Languages.Queries
{
    public class LanguageQueries : BaseQueries, ILanguageQueries
    {
        public async Task<CaptionViewModel> GetCaption(int captionId)
        {
            CaptionViewModel result = null;
            string cmd = $@"SELECT * FROM `caption` c 
                            LEFT JOIN `caption_language` cl ON c.id = cl.caption_id
                            WHERE c.id = {captionId}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                var rs = rd.Read<Caption, CaptionLanguage, CaptionViewModel>(
                    (cRs, clRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Caption, CaptionViewModel>(cRs);
                        }

                        if (clRs != null)
                        {
                            var language = result.Languages.FirstOrDefault(l => l.Id == clRs.Id);
                            if (language == null)
                            {
                                result.Languages.Add(clRs);
                            }
                        }

                        return result;

                    });

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    var rs = rd.Read<Caption, CaptionLanguage, CaptionViewModel>(
                        (cRs, clRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Caption, CaptionViewModel>(cRs);
                            }

                            if (clRs != null)
                            {
                                var language = result.Languages.FirstOrDefault(l => l.Id == clRs.Id);
                                if (language == null)
                                {
                                    result.Languages.Add(clRs);
                                }
                            }

                            return result;

                        });

                    return result;
                }
            }
        }

        public async Task<IEnumerable<CaptionViewModel>> GetCaptions()
        {
            List<CaptionViewModel> result = new List<CaptionViewModel>();
            string cmd = $@"SELECT * FROM `caption` c 
                            LEFT JOIN `caption_language` cl ON c.id = cl.caption_id";
            if(DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                var rs = rd.Read<Caption, CaptionLanguage, CaptionViewModel>(
                    (cRs, clRs) =>
                    {
                        var caption = result.FirstOrDefault(c => c.Id == cRs.Id);
                        if(caption == null)
                        {
                            caption = CommonHelper.Mapper<Caption, CaptionViewModel>(cRs);
                            result.Add(caption);
                        }

                        if (clRs != null)
                        {
                            var language = caption.Languages.FirstOrDefault(l => l.Id == clRs.Id);
                            if(language == null)
                            {
                                caption.Languages.Add(clRs);
                            }
                        }

                        return caption;

                    });

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    var rs = rd.Read<Caption, CaptionLanguage, CaptionViewModel>(
                        (cRs, clRs) =>
                        {
                            var caption = result.FirstOrDefault(c => c.Id == cRs.Id);
                            if (caption == null)
                            {
                                caption = CommonHelper.Mapper<Caption, CaptionViewModel>(cRs);
                                result.Add(caption);
                            }

                            if (clRs != null)
                            {
                                var language = caption.Languages.FirstOrDefault(l => l.Id == clRs.Id);
                                if (language == null)
                                {
                                    caption.Languages.Add(clRs);
                                }
                            }

                            return caption;

                        });
                    return result;
                }
            }
        }

        public async Task<IEnumerable<Language>> Gets(string condition = "")
        {
            string cmd = QueriesCreatingHelper.CreateQuerySelect<Language>(condition);
            return await DALHelper.ExecuteQuery<Language>(cmd, dbTransaction: DbTransaction, connection: DbConnection);           
        }
    }
}
