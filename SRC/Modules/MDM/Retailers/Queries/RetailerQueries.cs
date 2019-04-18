using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Interfaces;
using MDM.UI.Retailers.Models;
using MDM.UI.Retailers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Retailers.Queries
{
    public class RetailerQueries : BaseQueries, IRetailerQueries
    {
        private const string LocationCodeFormat = "RL{0}";
        public async Task<string> GenarateLocationCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(GLN) FROM `retailer_location`");
            if (previousCode == null)
            {
                code = LocationCodeFormat.Replace("{0}", 1.ToString("00000000"));
            }
            else
            {
                code = LocationCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(2, 8)) + 1).ToString("00000000"));
            }

            return code;
        }

        public async Task<RetailerViewModel> Get(int id)
        {
            RetailerViewModel result = null;
            string cmd = $@"SELECT * FROM `retailer` r
                            LEFT JOIN `address` a ON a.id = r.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = r.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            LEFT JOIN `retailer_location` rl ON rl.retailer_id = r.id AND rl.is_used = 1 AND rl.is_deleted = 0
                            WHERE r.id = {id} and r.is_deleted = 0 AND r.is_used = 1";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Retailer, Address, Contact, RetailerLocation, RetailerViewModel>(
                    (rRs, aRs, cRs, rlRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Retailer, RetailerViewModel>(rRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if (result.Contact == null)
                        {
                            result.Contact = cRs;
                        }

                        if(rlRs != null)
                        {
                            var location = result.Locations.FirstOrDefault(l => l.Id == rlRs.Id);
                            if(location == null)
                            {
                                result.Locations.Add(CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs));
                            }
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
                    rd.Read<Retailer, Address, Contact, RetailerLocation, RetailerViewModel>(
                        (rRs, aRs, cRs, rlRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Retailer, RetailerViewModel>(rRs);
                            }

                            if (result.Address == null)
                            {
                                result.Address = aRs;
                            }

                            if (result.Contact == null)
                            {
                                result.Contact = cRs;
                            }

                            if (rlRs != null)
                            {
                                var location = result.Locations.FirstOrDefault(l => l.Id == rlRs.Id);
                                if (location == null)
                                {
                                    result.Locations.Add(CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs));
                                }
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<RetailerViewModel> GetByUserId(int userId)
        {
            RetailerViewModel result = null;
            string cmd = $@"SELECT * FROM `retailer` r
                            LEFT JOIN `address` a ON a.id = r.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = r.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            LEFT JOIN `retailer_location` rl ON rl.retailer_id = r.id AND rl.is_used = 1 AND rl.is_deleted = 0
                            WHERE r.user_account_id = {userId} and r.is_deleted = 0 AND r.is_used = 1";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Retailer, Address, Contact, RetailerLocation, RetailerViewModel>(
                    (rRs, aRs, cRs, rlRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Retailer, RetailerViewModel>(rRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if (result.Contact == null)
                        {
                            result.Contact = cRs;
                        }

                        if (rlRs != null)
                        {
                            var location = result.Locations.FirstOrDefault(l => l.Id == rlRs.Id);
                            if (location == null)
                            {
                                result.Locations.Add(CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs));
                            }
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
                    rd.Read<Retailer, Address, Contact, RetailerLocation, RetailerViewModel>(
                        (rRs, aRs, cRs, rlRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Retailer, RetailerViewModel>(rRs);
                            }

                            if (result.Address == null)
                            {
                                result.Address = aRs;
                            }

                            if (result.Contact == null)
                            {
                                result.Contact = cRs;
                            }

                            if (rlRs != null)
                            {
                                var location = result.Locations.FirstOrDefault(l => l.Id == rlRs.Id);
                                if (location == null)
                                {
                                    result.Locations.Add(CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs));
                                }
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<RetailerLocationViewModel> GetRetailerLocation(int retailerLocationId)
        {
            RetailerLocationViewModel result = null;
            string cmd = $@"SELECT * FROM `retailer_location` rl
                            LEFT JOIN `address` a ON a.id = rl.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = rl.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE rl.id = {retailerLocationId} and rl.is_deleted = 0 AND rl.is_used = 1";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerLocation, Address, Contact, RetailerLocationViewModel>(
                    (rlRs, aRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if (result.Contact == null)
                        {
                            result.Contact = cRs;
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
                    rd.Read<RetailerLocation, Address, Contact, RetailerLocationViewModel>(
                        (rlRs, aRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs);
                            }

                            if (result.Address == null)
                            {
                                result.Address = aRs;
                            }

                            if (result.Contact == null)
                            {
                                result.Contact = cRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<RetailerLocationViewModel>> GetRetailerLocations(int retailerId)
        {
            List<RetailerLocationViewModel> result = new List<RetailerLocationViewModel>();
            string cmd = $@"SELECT * FROM `retailer_location` rl
                            LEFT JOIN `address` a ON a.id = rl.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = rl.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE rl.retailer_id = {retailerId} and rl.is_deleted = 0 AND rl.is_used = 1";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerLocation, Address, Contact, RetailerLocationViewModel>(
                    (rlRs, aRs, cRs) =>
                    {
                        var location = result.FirstOrDefault(l => l.Id == rlRs.Id);
                        if (location == null)
                        {
                            location = CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs);
                            result.Add(location);
                        }

                        if (location.Address == null)
                        {
                            location.Address = aRs;
                        }

                        if (location.Contact == null)
                        {
                            location.Contact = cRs;
                        }

                        return location;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<RetailerLocation, Address, Contact, RetailerLocationViewModel>(
                        (rlRs, aRs, cRs) =>
                        {
                            var location = result.FirstOrDefault(l => l.Id == rlRs.Id);
                            if (location == null)
                            {
                                location = CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs);
                                result.Add(location);
                            }

                            if (location.Address == null)
                            {
                                location.Address = aRs;
                            }

                            if (location.Contact == null)
                            {
                                location.Contact = cRs;
                            }

                            return location;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<RetailerLocationViewModel>> GetRetailerLocationsByUser(int userId)
        {
            List<RetailerLocationViewModel> result = new List<RetailerLocationViewModel>();
            string cmd = $@"SELECT * FROM `retailer_location` rl
                            LEFT JOIN `address` a ON a.id = rl.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = rl.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE rl.is_deleted = 0 AND rl.is_used = 1 and 
                                  rl.retailer_id = (SELECT r.id FROM `retailer` r WHERE user_account_id = {userId})";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerLocation, Address, Contact, RetailerLocationViewModel>(
                    (rlRs, aRs, cRs) =>
                    {
                        var location = result.FirstOrDefault(l => l.Id == rlRs.Id);
                        if (location == null)
                        {
                            location = CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs);
                            result.Add(location);
                        }

                        if (location.Address == null)
                        {
                            location.Address = aRs;
                        }

                        if (location.Contact == null)
                        {
                            location.Contact = cRs;
                        }

                        return location;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<RetailerLocation, Address, Contact, RetailerLocationViewModel>(
                        (rlRs, aRs, cRs) =>
                        {
                            var location = result.FirstOrDefault(l => l.Id == rlRs.Id);
                            if (location == null)
                            {
                                location = CommonHelper.Mapper<RetailerLocation, RetailerLocationViewModel>(rlRs);
                                result.Add(location);
                            }

                            if (location.Address == null)
                            {
                                location.Address = aRs;
                            }

                            if (location.Contact == null)
                            {
                                location.Contact = cRs;
                            }

                            return location;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<RetailerViewModel>> Gets(string condition = "")
        {
            List<RetailerViewModel> result = new List<RetailerViewModel>();
            string cmd = $@"SELECT * FROM `retailer` r 
                            LEFT JOIN `address` a ON r.address_id = a.id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON r.contact_id = c.id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE r.is_used = 1 AND r.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Retailer, Address, Contact, RetailerViewModel>(
                    (rRs, aRs, cRs) =>
                    {
                        var retailer = result.FirstOrDefault(r => r.Id == rRs.Id);
                        if (retailer == null)
                        {
                            retailer = CommonHelper.Mapper<Retailer, RetailerViewModel>(rRs);
                            result.Add(retailer);
                        }

                        if (retailer.Address == null)
                        {
                            retailer.Address = aRs;
                        }

                        if (retailer.Contact == null)
                        {
                            retailer.Contact = cRs;
                        }

                        return retailer;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Retailer, Address, Contact, RetailerViewModel>(
                        (rRs, aRs, cRs) =>
                        {
                            var retailer = result.FirstOrDefault(r => r.Id == rRs.Id);
                            if (retailer == null)
                            {
                                retailer = CommonHelper.Mapper<Retailer, RetailerViewModel>(rRs);
                                result.Add(retailer);
                            }

                            if (retailer.Address == null)
                            {
                                retailer.Address = aRs;
                            }

                            if (retailer.Contact == null)
                            {
                                retailer.Contact = cRs;
                            }

                            return retailer;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
