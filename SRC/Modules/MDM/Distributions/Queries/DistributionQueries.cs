using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using MDM.UI.Distributions.ViewModels;
using MDM.UI.Geographical.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace MDM.Distributions.Queries
{
    public class DistributionQueries : BaseQueries, IDistributionQueries
    {
        private const string DistributionCodeFormat = "D{0}";
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `distribution`");
            if (previousCode == null)
            {
                code = DistributionCodeFormat.Replace("{0}", 1.ToString("000000000"));
            }
            else
            {
                code = DistributionCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(1, 9)) + 1).ToString("000000000"));
            }

            return code;
        }

        public async Task<IEnumerable<DistributionViewModel>> GetsByEmployeeId(int employeeId)
        {
            List<DistributionViewModel> result = new List<DistributionViewModel>();
            string cmd = $@"SELECT * FROM (SELECT * FROM `distribution` dd
		                            	   WHERE dd.manager_id = {employeeId} or EXISTS(SELECT Id FROM `distribution_employee` de 
		                            										     WHERE de.distribution_id = dd.Id AND de.employee_id = {employeeId})) d
		                             LEFT JOIN `address` a ON a.id = d.address_id AND a.is_used = 1 AND a.is_deleted = 0
		                             LEFT JOIN `contact` cc ON cc.id = d.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
		                             WHERE d.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                    (cRs, aRs, ccRs) =>
                    {
                        var distribution = result.FirstOrDefault(c => c.Id == cRs.Id);
                        if (distribution == null)
                        {
                            distribution = CommonHelper.Mapper<Distribution, DistributionViewModel>(cRs);
                            result.Add(distribution);
                        }

                        if (distribution.Address == null)
                        {
                            distribution.Address = aRs;
                        }

                        if (distribution.Contact == null)
                        {
                            distribution.Contact = ccRs;
                        }

                        return distribution;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                        (cRs, aRs, ccRs) =>
                        {
                            var distribution = result.FirstOrDefault(c => c.Id == cRs.Id);
                            if (distribution == null)
                            {
                                distribution = CommonHelper.Mapper<Distribution, DistributionViewModel>(cRs);
                                result.Add(distribution);
                            }

                            if (distribution.Address == null)
                            {
                                distribution.Address = aRs;
                            }

                            if (distribution.Contact == null)
                            {
                                distribution.Contact = ccRs;
                            }

                            return distribution;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<DistributionViewModel>> Gets(string condition = "")
        {
            List<DistributionViewModel> result = new List<DistributionViewModel>();
            string cmd = $@"SELECT * FROM `distribution` d
                            LEFT JOIN `address` a ON a.id = d.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = d.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE d.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                    (dRs, aRs, cdRs) =>
                    {
                        var distribution = result.FirstOrDefault(c => c.Id == dRs.Id);
                        if (distribution == null)
                        {
                            distribution = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                            result.Add(distribution);
                        }

                        if (distribution.Address == null)
                        {
                            distribution.Address = aRs;
                        }

                        if (distribution.Contact == null)
                        {
                            distribution.Contact = cdRs;
                        }

                        return distribution;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                        (dRs, aRs, cdRs) =>
                        {
                            var distribution = result.FirstOrDefault(c => c.Id == dRs.Id);
                            if (distribution == null)
                            {
                                distribution = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                                result.Add(distribution);
                            }
                    
                            if (distribution.Address == null)
                            {
                                distribution.Address = aRs;
                            }
                    
                            if (distribution.Contact == null)
                            {
                                distribution.Contact = cdRs;
                            }
                    
                            return distribution;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<DistributionViewModel> Get(int id)
        {
            DistributionViewModel result = null;
            string cmd = $@"SELECT * FROM `distribution` d
                            LEFT JOIN `address` a ON a.id = d.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = d.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE d.id = {id} and d.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                    (dRs, aRs, cdRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if (result.Contact == null)
                        {
                            result.Contact = cdRs;
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
                    rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                        (dRs, aRs, cdRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                            }

                            if (result.Address == null)
                            {
                                result.Address = aRs;
                            }

                            if (result.Contact == null)
                            {
                                result.Contact = cdRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<DistributionViewModel> GetByCode(string code)
        {
            DistributionViewModel result = null;
            string cmd = $@"SELECT * FROM `distribution` d
                            LEFT JOIN `address` a ON a.id = d.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = d.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE d.code = '{code}' and d.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                    (dRs, aRs, cdRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if (result.Contact == null)
                        {
                            result.Contact = cdRs;
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
                    rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                        (dRs, aRs, cdRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                            }

                            if (result.Address == null)
                            {
                                result.Address = aRs;
                            }

                            if (result.Contact == null)
                            {
                                result.Contact = cdRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<DistributionViewModel>> GetsBySupervisor(int managerId)
        {
            List<DistributionViewModel> result = new List<DistributionViewModel>();
            string cmd = $@"SELECT * FROM `distribution` d
                            LEFT JOIN `address` a ON a.id = d.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = d.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE d.is_deleted = 0 and d.manager_id = {managerId}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                    (dRs, aRs, cdRs) =>
                    {
                        var distribution = result.FirstOrDefault(c => c.Id == dRs.Id);
                        if (distribution == null)
                        {
                            distribution = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                            result.Add(distribution);
                        }

                        if (distribution.Address == null)
                        {
                            distribution.Address = aRs;
                        }

                        if (distribution.Contact == null)
                        {
                            distribution.Contact = cdRs;
                        }

                        return distribution;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Distribution, Address, Contact, DistributionViewModel>(
                        (dRs, aRs, cdRs) =>
                        {
                            var distribution = result.FirstOrDefault(c => c.Id == dRs.Id);
                            if (distribution == null)
                            {
                                distribution = CommonHelper.Mapper<Distribution, DistributionViewModel>(dRs);
                                result.Add(distribution);
                            }

                            if (distribution.Address == null)
                            {
                                distribution.Address = aRs;
                            }

                            if (distribution.Contact == null)
                            {
                                distribution.Contact = cdRs;
                            }

                            return distribution;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
