using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Distributions.UI.Interfaces;
using Distributions.UI.Models;
using Distributions.UI.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.Queries
{
    public class RouterQueries : BaseQueries, IRouterQueries
    {
        public async Task<RouterViewModel> Get(int id)
        {
            RouterViewModel result = null;
            string cmd = $@"SELECT * FROM `router` r
                            LEFT JOIN `country` c ON c.id = r.country_id AND c.is_used = 1 AND c.is_deleted = 0
                            LEFT JOIN `province` p ON p.id = r.province_id AND p.is_used = 1 AND p.is_deleted = 0
                            WHERE r.is_deleted = 0 and r.id = {id}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Router, Country, Province, RouterViewModel>(
                    (rRs, cRs, pRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
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
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Router, Country, Province, RouterViewModel>(
                        (rRs, cRs, pRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
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

        public async Task<IEnumerable<RouterViewModel>> Gets(string conditions)
        {
            List<RouterViewModel> result = new List<RouterViewModel>();
            string cmd = $@"SELECT * FROM `router` r
                            LEFT JOIN `country` c ON c.id = r.country_id AND c.is_used = 1 AND c.is_deleted = 0
                            LEFT JOIN `province` p ON p.id = r.province_id AND p.is_used = 1 AND p.is_deleted = 0
                            WHERE r.is_deleted = 0";
            if (!string.IsNullOrEmpty(conditions))
            {
                cmd += " AND " + conditions;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Router, Country, Province, RouterViewModel>(
                    (rRs, cRs, pRs) =>
                    {
                        var router = result.FirstOrDefault(r => r.Id == rRs.Id);
                        if (router == null)
                        {
                            router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                            result.Add(router);
                        }

                        if (router.Country == null)
                        {
                            router.Country = cRs;
                        }

                        if (router.Province == null)
                        {
                            router.Province = pRs;
                        }

                        return router;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Router, Country, Province, RouterViewModel>(
                        (rRs, cRs, pRs) =>
                        {
                            var router = result.FirstOrDefault(r => r.Id == rRs.Id);
                            if (router == null)
                            {
                                router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                                result.Add(router);
                            }

                            if (router.Country == null)
                            {
                                router.Country = cRs;
                            }

                            if (router.Province == null)
                            {
                                router.Province = pRs;
                            }

                            return router;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<RouterViewModel>> Gets(int distributionId)
        {
            List<RouterViewModel> result = new List<RouterViewModel>();
            string cmd = $@"SELECT * FROM `router` r
                            LEFT JOIN `country` c ON c.id = r.country_id AND c.is_used = 1 AND c.is_deleted = 0
                            LEFT JOIN `province` p ON p.id = r.province_id AND p.is_used = 1 AND p.is_deleted = 0
                            WHERE r.is_deleted = 0 and r.distribution_id = {distributionId}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Router, Country, Province, RouterViewModel>(
                    (rRs, cRs, pRs) =>
                    {
                        var router = result.FirstOrDefault(r => r.Id == rRs.Id);
                        if (router == null)
                        {
                            router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                            result.Add(router);
                        }

                        if (router.Country == null)
                        {
                            router.Country = cRs;
                        }

                        if (router.Province == null)
                        {
                            router.Province = pRs;
                        }

                        return router;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Router, Country, Province, RouterViewModel>(
                        (rRs, cRs, pRs) =>
                        {
                            var router = result.FirstOrDefault(r => r.Id == rRs.Id);
                            if (router == null)
                            {
                                router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                                result.Add(router);
                            }

                            if (router.Country == null)
                            {
                                router.Country = cRs;
                            }

                            if (router.Province == null)
                            {
                                router.Province = pRs;
                            }

                            return router;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
