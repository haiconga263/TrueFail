using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Collections.ViewModels;
using MDM.UI.Fulfillments.Interfaces;
using MDM.UI.Fulfillments.Models;
using MDM.UI.Fulfillments.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Fulfillments.Queries
{
    public class FulfillmentQueries : BaseQueries, IFulfillmentQueries
    {
        private const string FulfillmentCodeFormat = "F{0}";
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `fulfillment`");
            if (previousCode == null)
            {
                code = FulfillmentCodeFormat.Replace("{0}", 1.ToString("000000000"));
            }
            else
            {
                code = FulfillmentCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(1, 9)) + 1).ToString("000000000"));
            }

            return code;
        }
        public async Task<IEnumerable<FulfillmentViewModel>> Gets(string condition = "")
        {
            List<FulfillmentViewModel> result = new List<FulfillmentViewModel>();
            string cmd = $@"SELECT * FROM `fulfillment` f
                            LEFT JOIN `address` a ON a.id = f.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = f.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE f.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                    (fRs, aRs, ccRs) =>
                    {
                        var fulfillment = result.FirstOrDefault(c => c.Id == fRs.Id);
                        if (fulfillment == null)
                        {
                            fulfillment = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                            result.Add(fulfillment);
                        }

                        if (fulfillment.Address == null)
                        {
                            fulfillment.Address = aRs;
                        }

                        if (fulfillment.Contact == null)
                        {
                            fulfillment.Contact = ccRs;
                        }

                        return fulfillment;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                        (fRs, aRs, ccRs) =>
                        {
                            var fulfillment = result.FirstOrDefault(c => c.Id == fRs.Id);
                            if (fulfillment == null)
                            {
                                fulfillment = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                                result.Add(fulfillment);
                            }

                            if (fulfillment.Address == null)
                            {
                                fulfillment.Address = aRs;
                            }

                            if (fulfillment.Contact == null)
                            {
                                fulfillment.Contact = ccRs;
                            }

                            return fulfillment;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<FulfillmentViewModel> Get(int id)
        {
            FulfillmentViewModel result = null;
            string cmd = $@"SELECT * FROM `fulfillment` f
                            LEFT JOIN `address` a ON a.id = f.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = f.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE f.id = {id} and f.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                    (fRs, aRs, ccRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if (result.Contact == null)
                        {
                            result.Contact = ccRs;
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
                    rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                        (fRs, aRs, ccRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                            }

                            if (result.Address == null)
                            {
                                result.Address = aRs;
                            }

                            if (result.Contact == null)
                            {
                                result.Contact = ccRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<FulfillmentViewModel> GetByCode(string code)
        {
            FulfillmentViewModel result = null;
            string cmd = $@"SELECT * FROM `fulfillment` f
                            LEFT JOIN `address` a ON a.id = f.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = f.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE f.code = '{code}' and f.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                    (fRs, aRs, ccRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if (result.Contact == null)
                        {
                            result.Contact = ccRs;
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
                    rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                        (fRs, aRs, ccRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                            }

                            if (result.Address == null)
                            {
                                result.Address = aRs;
                            }

                            if (result.Contact == null)
                            {
                                result.Contact = ccRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<FulfillmentViewModel>> GetsBySupervisor(int managerId)
        {
            List<FulfillmentViewModel> result = new List<FulfillmentViewModel>();
            string cmd = $@"SELECT * FROM `fulfillment` f
                            LEFT JOIN `address` a ON a.id = f.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = f.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE f.is_deleted = 0 f.manager_id = {managerId}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                    (fRs, aRs, ccRs) =>
                    {
                        var fulfillment = result.FirstOrDefault(c => c.Id == fRs.Id);
                        if (fulfillment == null)
                        {
                            fulfillment = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                            result.Add(fulfillment);
                        }

                        if (fulfillment.Address == null)
                        {
                            fulfillment.Address = aRs;
                        }

                        if (fulfillment.Contact == null)
                        {
                            fulfillment.Contact = ccRs;
                        }

                        return fulfillment;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Fulfillment, Address, Contact, FulfillmentViewModel>(
                        (fRs, aRs, ccRs) =>
                        {
                            var fulfillment = result.FirstOrDefault(c => c.Id == fRs.Id);
                            if (fulfillment == null)
                            {
                                fulfillment = CommonHelper.Mapper<Fulfillment, FulfillmentViewModel>(fRs);
                                result.Add(fulfillment);
                            }

                            if (fulfillment.Address == null)
                            {
                                fulfillment.Address = aRs;
                            }

                            if (fulfillment.Contact == null)
                            {
                                fulfillment.Contact = ccRs;
                            }

                            return fulfillment;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<Fulfillment>> GetAllFufillmentAsync()
        {
            List<Fulfillment> result = new List<Fulfillment>();
            string cmd = $@"SELECT * FROM `fulfillment`                             
                            WHERE is_deleted = 0";
            return await DALHelper.Query<Fulfillment>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
        
    }
}
