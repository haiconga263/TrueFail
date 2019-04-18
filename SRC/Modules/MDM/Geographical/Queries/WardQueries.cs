using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Geographical.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Geographical.Queries
{
    public class WardQueries : BaseQueries, IWardQueries
    {
        public async Task<WardViewModel> Get(int wardId)
        {
            WardViewModel result = null;
            string cmd = $@"SELECT * FROM `ward` w
                            LEFT JOIN `district` d ON w.district_id = d.id AND d.is_used = 1
                            LEFT JOIN `province` p ON w.province_id = p.id AND p.is_used = 1
                            LEFT JOIN `country` c ON w.country_id = c.id AND c.is_used = 1
                            WHERE w.`id` = {wardId} AND w.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Ward, District, Province, Country, WardViewModel>(
                    (wRs, dRs, pRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Ward, WardViewModel>(wRs);
                        }

                        if (result.Country == null)
                        {
                            result.Country = cRs;
                        }

                        if (result.Province == null)
                        {
                            result.Province = pRs;
                        }

                        if (result.District == null)
                        {
                            result.District = dRs;
                        }

                        return result;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<Ward, District, Province, Country, WardViewModel>(
                        (wRs, dRs, pRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Ward, WardViewModel>(wRs);
                            }

                            if (result.Country == null)
                            {
                                result.Country = cRs;
                            }

                            if (result.Province == null)
                            {
                                result.Province = pRs;
                            }

                            if (result.District == null)
                            {
                                result.District = dRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<WardCommon>> GetCommons()
        {
            string cmd = "SELECT * FROM `ward` WHERE is_used = 1 AND is_deleted = 0";
            return await DALHelper.Query<WardCommon>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<WardViewModel>> Gets(string condition = "")
        {
            List<WardViewModel> result = new List<WardViewModel>();
            string cmd = $@"SELECT * FROM `ward` w
                            LEFT JOIN `district` d ON w.district_id = d.id AND d.is_used = 1
                            LEFT JOIN `province` p ON w.province_id = p.id AND p.is_used = 1
                            LEFT JOIN `country` c ON w.country_id = c.id AND c.is_used = 1
                            WHERE w.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Ward, District, Province, Country, WardViewModel>(
                    (wRs, dRs, pRs, cRs) =>
                    {
                        var ward = result.FirstOrDefault(o => o.Id == wRs.Id);

                        if (ward == null)
                        {
                            ward = CommonHelper.Mapper<Ward, WardViewModel>(wRs);
                            result.Add(ward);
                        }

                        if (ward.Country == null)
                        {
                            ward.Country = cRs;
                        }

                        if (ward.Province == null)
                        {
                            ward.Province = pRs;
                        }

                        if (ward.District == null)
                        {
                            ward.District = dRs;
                        }

                        return ward;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<Ward, District, Province, Country, WardViewModel>(
                        (wRs, dRs, pRs, cRs) =>
                        {
                            var ward = result.FirstOrDefault(o => o.Id == wRs.Id);

                            if (ward == null)
                            {
                                ward = CommonHelper.Mapper<Ward, WardViewModel>(wRs);
                                result.Add(ward);
                            }

                            if (ward.Country == null)
                            {
                                ward.Country = cRs;
                            }

                            if (ward.Province == null)
                            {
                                ward.Province = pRs;
                            }

                            if (ward.District == null)
                            {
                                ward.District = dRs;
                            }

                            return ward;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
