using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Farmers.Models;
using MDM.UI.Farmers.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Farmers.Queries
{
    public class FarmerQueries : BaseQueries, IFarmerQueries
    {
        public async Task<FarmerViewModel> Get(int id)
        {
            FarmerViewModel result = null;
            string cmd = $@"SELECT * FROM `farmer` f
                            LEFT JOIN `address` a ON a.id = f.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = f.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE f.id = {id} and f.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Farmer, Address, Contact, FarmerViewModel>(
                    (fRs, aRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Farmer, FarmerViewModel>(fRs);
                        }

                        if (result.Address == null)
                        {
                            result.Address = aRs;
                        }

                        if(result.Contact == null)
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
                    rd.Read<Farmer, Address, Contact, FarmerViewModel>(
                        (fRs, aRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Farmer, FarmerViewModel>(fRs);
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

        public async Task<FarmerViewModel> GetByUser(int userId)
        {
            FarmerViewModel result = null;
            string cmd = $@"SELECT * FROM `farmer` f
                            LEFT JOIN `address` a ON a.id = f.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = f.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE f.user_account_id = {userId} and f.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Farmer, Address, Contact, FarmerViewModel>(
                    (fRs, aRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Farmer, FarmerViewModel>(fRs);
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
                    rd.Read<Farmer, Address, Contact, FarmerViewModel>(
                        (fRs, aRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Farmer, FarmerViewModel>(fRs);
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

        public async Task<IEnumerable<FarmerViewModel>> Gets(string condition = "")
        {
            List<FarmerViewModel> result = new List<FarmerViewModel>();
            string cmd = $@"SELECT * FROM `farmer` f
                            LEFT JOIN `address` a ON a.id = f.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = f.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE f.is_deleted = 0 ORDER BY f.created_date";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Farmer, Address, Contact, FarmerViewModel>(
                    (fRs, aRs, cRs) =>
                    {
                        var farmer = result.FirstOrDefault(f => f.Id == fRs.Id);
                        if (farmer == null)
                        {
                            farmer = CommonHelper.Mapper<Farmer, FarmerViewModel>(fRs);
                            result.Add(farmer);
                        }

                        if(farmer.Address == null)
                        {
                            farmer.Address = aRs;
                        }

                        if (farmer.Contact == null)
                        {
                            farmer.Contact = cRs;
                        }

                        return farmer;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Farmer, Address, Contact, FarmerViewModel>(
                        (fRs, aRs, cRs) =>
                        {
                            var farmer = result.FirstOrDefault(f => f.Id == fRs.Id);
                            if (farmer == null)
                            {
                                farmer = CommonHelper.Mapper<Farmer, FarmerViewModel>(fRs);
                                result.Add(farmer);
                            }

                            if (farmer.Address == null)
                            {
                                farmer.Address = aRs;
                            }

                            if (farmer.Contact == null)
                            {
                                farmer.Contact = cRs;
                            }

                            return farmer;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
