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
    public class ProvinceQueries : BaseQueries, IProvinceQueries
    {
        public async Task<ProvinceViewModel> Get(int provinceId)
        {
            ProvinceViewModel result = null;
            string cmd = $@"SELECT * FROM `province` p
                            LEFT JOIN `region` r ON p.region_id = r.id AND r.is_used = 1
                            LEFT JOIN `country` c ON p.country_id = c.id AND c.is_used = 1
                            WHERE p.`id` = {provinceId} AND p.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Province, Region, Country, ProvinceViewModel>(
                    (pRs, rRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Province, ProvinceViewModel>(pRs);
                        }

                        if (result.Country == null)
                        {
                            result.Country = cRs;
                        }

                        if (result.Region == null)
                        {
                            result.Region = rRs;
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
                    rd.Read<Province, Region, Country, ProvinceViewModel>(
                    (pRs, rRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Province, ProvinceViewModel>(pRs);
                            }

                            if (result.Country == null)
                            {
                                result.Country = cRs;
                            }

                            if (result.Region == null)
                            {
                                result.Region = rRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<ProvinceCommon>> GetCommons()
        {
            string cmd = "SELECT * FROM `province` WHERE is_used = 1 AND is_deleted = 0";
            return await DALHelper.Query<ProvinceCommon>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<ProvinceViewModel>> Gets(string condition = "")
        {
            List<ProvinceViewModel> result = new List<ProvinceViewModel>();
            string cmd = $@"SELECT * FROM `province` p
                            LEFT JOIN `region` r ON p.region_id = r.id AND r.is_used = 1
                            LEFT JOIN `country` c ON p.country_id = c.id AND c.is_used = 1
                            WHERE p.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Province, Region, Country, ProvinceViewModel>(
                    (pRs, rRs, cRs) =>
                    {
                        var province = result.FirstOrDefault(o => o.Id == pRs.Id);

                        if (province == null)
                        {
                            province = CommonHelper.Mapper<Province, ProvinceViewModel>(pRs);
                            result.Add(province);
                        }

                        if (province.Country == null)
                        {
                            province.Country = cRs;
                        }

                        if (province.Region == null)
                        {
                            province.Region = rRs;
                        }

                        return province;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<Province, Region, Country, ProvinceViewModel>(
                    (pRs, rRs, cRs) =>
                        {
                            var province = result.FirstOrDefault(o => o.Id == pRs.Id);

                            if (province == null)
                            {
                                province = CommonHelper.Mapper<Province, ProvinceViewModel>(pRs);
                                result.Add(province);
                            }

                            if (province.Country == null)
                            {
                                province.Country = cRs;
                            }

                            if (province.Region == null)
                            {
                                province.Region = rRs;
                            }

                            return province;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
