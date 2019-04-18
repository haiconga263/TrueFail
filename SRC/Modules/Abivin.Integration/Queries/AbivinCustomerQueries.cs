using Abivin.Integration.UI.Interfaces;
using Abivin.Integration.UI.ViewModels;
using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Models;
using MDM.UI.Retailers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abivin.Integration.Queries
{
    public class AbivinCustomerQueries : BaseQueries, IAbivinCustomerQueries
    {
        public async Task<CustomerViewModel> Get(string code)
        {
            CustomerViewModel result = null;
            string cmd = $@"SELECT * FROM `retailer_location` rl
                            LEFT JOIN `address` a ON a.id = rl.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `province` p ON p.id = a.province_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = rl.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE rl.is_deleted = 0 AND rl.is_used = 1 and rl.GLN = '{code}'";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerLocation, Address, Province, Contact, CustomerViewModel>(
                    (rlRs, aRs, pRs, cRs) =>
                    {
                        if (result == null)
                        {
                            result = new CustomerViewModel()
                            {
                                OrganizationCode = "", //hardcode
                                PartnerCode = rlRs.GLN,
                                PartnerName = rlRs.Name,
                                PartnerGroupCode = "CUSTOMER", //hardcode
                                Title = cRs == null ? string.Empty : ("M".Equals(cRs.Gender) ? "Mr." : "Mrs."),
                                PhoneNumber = cRs == null ? string.Empty : cRs.Phone,
                                City = pRs == null ? string.Empty : pRs.Name,
                                Address = aRs == null ? string.Empty : aRs.Street,
                                Latitude = aRs == null ? 0 : aRs.Latitude,
                                Longitude = aRs == null ? 0 : aRs.Longitude,
                                Email = cRs == null ? string.Empty : cRs.Email,
                                OpenTime = "00:00", //hardcode
                                CloseTime = "23:59", //hardcode
                                LunchTime = "12:00", //hardcode
                                MinTime = "00:15", //hardcode
                                MaxTime = "00:30", //hardcode
                                TimeWindow = "0", //hardcode
                                BikeOnly = false,
                                TruckOnly = false,
                                SalesCode = string.Empty,
                                SerialNumber = string.Empty,
                                Comment = string.Empty,
                            };
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
                    rd.Read<RetailerLocation, Address, Province, Contact, CustomerViewModel>(
                        (rlRs, aRs, pRs, cRs) =>
                        {
                            if (result == null)
                            {
                                result = new CustomerViewModel()
                                {
                                    OrganizationCode = "", //hardcode
                                    PartnerCode = rlRs.GLN,
                                    PartnerName = rlRs.Name,
                                    PartnerGroupCode = "CUSTOMER", //hardcode
                                    Title = cRs == null ? string.Empty : ("M".Equals(cRs.Gender) ? "Mr." : "Mrs."),
                                    PhoneNumber = cRs == null ? string.Empty : cRs.Phone,
                                    City = pRs == null ? string.Empty : pRs.Name,
                                    Address = aRs == null ? string.Empty : aRs.Street,
                                    Latitude = aRs == null ? 0 : aRs.Latitude,
                                    Longitude = aRs == null ? 0 : aRs.Longitude,
                                    Email = cRs == null ? string.Empty : cRs.Email,
                                    OpenTime = "00:00", //hardcode
                                    CloseTime = "23:59", //hardcode
                                    LunchTime = "12:00", //hardcode
                                    MinTime = "00:15", //hardcode
                                    MaxTime = "00:30", //hardcode
                                    TimeWindow = "0", //hardcode
                                    BikeOnly = false,
                                    TruckOnly = false,
                                    SalesCode = string.Empty,
                                    SerialNumber = string.Empty,
                                    Comment = string.Empty,
                                };
                            }
                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<CustomerViewModel>> Gets(string condition = "")
        {
            List<CustomerViewModel> result = new List<CustomerViewModel>();
            string cmd = $@"SELECT * FROM `retailer_location` rl
                            LEFT JOIN `address` a ON a.id = rl.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `province` p ON p.id = a.province_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `contact` c ON c.id = rl.contact_id AND c.is_used = 1 AND c.is_deleted = 0
                            WHERE rl.is_deleted = 0 AND rl.is_used = 1";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += $" and {condition}";
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerLocation, Address, Province, Contact, CustomerViewModel>(
                    (rlRs, aRs, pRs, cRs) =>
                    {
                        var location = result.FirstOrDefault(l => l.PartnerCode == rlRs.GLN);
                        if (location == null)
                        {
                            location = new CustomerViewModel()
                            {
                                OrganizationCode = "", //hardcode
                                PartnerCode = rlRs.GLN,
                                PartnerName = rlRs.Name,
                                PartnerGroupCode = "CUSTOMER", //hardcode
                                Title = cRs == null ? string.Empty : ("M".Equals(cRs.Gender) ? "Mr." : "Mrs."),
                                PhoneNumber = cRs == null ? string.Empty : cRs.Phone,
                                City = pRs == null ? string.Empty : pRs.Name,
                                Address = aRs == null ? string.Empty : aRs.Street,
                                Latitude = aRs == null ? 0 : aRs.Latitude,
                                Longitude = aRs == null ? 0 : aRs.Longitude,
                                Email = cRs == null ? string.Empty : cRs.Email,
                                OpenTime = "00:00", //hardcode
                                CloseTime = "23:59", //hardcode
                                LunchTime = "12:00", //hardcode
                                MinTime = "00:15", //hardcode
                                MaxTime = "00:30", //hardcode
                                TimeWindow = "0", //hardcode
                                BikeOnly = false,
                                TruckOnly = false,
                                SalesCode = string.Empty,
                                SerialNumber = string.Empty,
                                Comment = string.Empty,
                            };
                            result.Add(location);
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
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<RetailerLocation, Address, Province, Contact, CustomerViewModel>(
                        (rlRs, aRs, pRs, cRs) =>
                        {
                            var location = result.FirstOrDefault(l => l.PartnerCode == rlRs.GLN);
                            if (location == null)
                            {
                                location = new CustomerViewModel()
                                {
                                    PartnerCode = rlRs.GLN,
                                    PartnerName = rlRs.Name,
                                    PartnerGroupCode = "CUSTOMER", //hardcode
                                Title = cRs == null ? string.Empty : ("M".Equals(cRs.Gender) ? "Mr." : "Mrs."),
                                    PhoneNumber = cRs == null ? string.Empty : cRs.Phone,
                                    City = pRs == null ? string.Empty : pRs.Name,
                                    Address = aRs == null ? string.Empty : aRs.Street,
                                    Latitude = aRs == null ? 0 : aRs.Latitude,
                                    Longitude = aRs == null ? 0 : aRs.Longitude,
                                    Email = cRs == null ? string.Empty : cRs.Email,
                                    OpenTime = "00:00", //hardcode
                                CloseTime = "23:59", //hardcode
                                LunchTime = "12:00", //hardcode
                                MinTime = "00:15", //hardcode
                                MaxTime = "00:30", //hardcode
                                TimeWindow = "0", //hardcode
                                BikeOnly = false,
                                    TruckOnly = false,
                                    SalesCode = string.Empty,
                                    SerialNumber = string.Empty,
                                    Comment = string.Empty,
                                };
                                result.Add(location);
                            }
                            return location;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
