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
    public class DistrictQueries : BaseQueries, IDistrictQueries
    {
        public async Task<DistrictViewModel> Get(int districtId)
        {
            DistrictViewModel result = null;
            string cmd = $@"SELECT * FROM `district` d
                            LEFT JOIN `province` p ON d.province_id = p.id AND p.is_used = 1
                            LEFT JOIN `country` c ON d.country_id = c.id AND c.is_used = 1
                            WHERE d.`id` = {districtId} AND d.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<District, Province, Country, DistrictViewModel>(
                    (dRs, pRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<District, DistrictViewModel>(dRs);
                        }

                        if (result.Country == null)
                        {
                            result.Country = cRs;
                        }

                        if (result.Province == null)
                        {
                            result.Province = pRs;
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
                    rd.Read<District, Province, Country, DistrictViewModel>(
                        (dRs, pRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<District, DistrictViewModel>(dRs);
                            }

                            if (result.Country == null)
                            {
                                result.Country = cRs;
                            }

                            if (result.Province == null)
                            {
                                result.Province = pRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<DistrictCommon>> GetCommons()
        {
            string cmd = "SELECT * FROM `district` WHERE is_used = 1 AND is_deleted = 0";
            return await DALHelper.Query<DistrictCommon>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<DistrictViewModel>> Gets(string condition = "")
        {
            List<DistrictViewModel> result = new List<DistrictViewModel>();
            string cmd = $@"SELECT * FROM `district` d
                            LEFT JOIN `province` p ON d.province_id = p.id AND p.is_used = 1
                            LEFT JOIN `country` c ON d.country_id = c.id AND c.is_used = 1
                            WHERE d.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<District, Province, Country, DistrictViewModel>(
                    (dRs, pRs, cRs) =>
                    {
                        var district = result.FirstOrDefault(o => o.Id == dRs.Id);

                        if (district == null)
                        {
                            district = CommonHelper.Mapper<District, DistrictViewModel>(dRs);
                            result.Add(district);
                        }

                        if (district.Country == null)
                        {
                            district.Country = cRs;
                        }

                        if (district.Province == null)
                        {
                            district.Province = pRs;
                        }

                        return district;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<District, Province, Country, DistrictViewModel>(
                        (dRs, pRs, cRs) =>
                        {
                            var district = result.FirstOrDefault(o => o.Id == dRs.Id);

                            if (district == null)
                            {
                                district = CommonHelper.Mapper<District, DistrictViewModel>(dRs);
                                result.Add(district);
                            }

                            if (district.Country == null)
                            {
                                district.Country = cRs;
                            }

                            if (district.Province == null)
                            {
                                district.Province = pRs;
                            }

                            return district;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
