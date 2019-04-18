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
    public class RegionQueries : BaseQueries, IRegionQueries
    {
        public async Task<RegionViewModel> Get(int regionId)
        {
            RegionViewModel result = null;
            string cmd = $@"SELECT * FROM `region` r
                            LEFT JOIN `country` c ON r.country_id = c.id AND c.is_used = 1
                            WHERE r.`id` = {regionId} AND r.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Region, Country, RegionViewModel>(
                    (rRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Region, RegionViewModel>(rRs);
                        }

                        if (result.Country == null)
                        {
                            result.Country = cRs;
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
                    rd.Read<Region, Country, RegionViewModel>(
                        (rRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Region, RegionViewModel>(rRs);
                            }

                            if (result.Country == null)
                            {
                                result.Country = cRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<RegionCommon>> GetCommons()
        {
            string cmd = "SELECT * FROM `region` WHERE is_used = 1 AND is_deleted = 0";
            return await DALHelper.Query<RegionCommon>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<RegionViewModel>> Gets(string condition = "")
        {
            List<RegionViewModel> result = new List<RegionViewModel>();
            string cmd = $@"SELECT * FROM `region` r
                            LEFT JOIN `country` c ON r.country_id = c.id AND c.is_used = 1
                            WHERE r.is_deleted = 0";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                SqlMapper.GridReader rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Region, Country, RegionViewModel>(
                    (rRs, cRs) =>
                    {
                        var region = result.FirstOrDefault(o => o.Id == rRs.Id);

                        if (region == null)
                        {
                            region = CommonHelper.Mapper<Region, RegionViewModel>(rRs);
                            result.Add(region);
                        }

                        if (region.Country == null)
                        {
                            region.Country = cRs;
                        }

                        return region;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    SqlMapper.GridReader rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<Region, Country, RegionViewModel>(
                        (rRs, cRs) =>
                        {
                            var region = result.FirstOrDefault(o => o.Id == rRs.Id);

                            if (region == null)
                            {
                                region = CommonHelper.Mapper<Region, RegionViewModel>(rRs);
                                result.Add(region);
                            }

                            if (region.Country == null)
                            {
                                region.Country = cRs;
                            }

                            return region;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
