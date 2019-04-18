using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Vehicles.Interfaces;
using MDM.UI.Vehicles.Models;
using MDM.UI.Vehicles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Vehicles.Queries
{
    public class VehicleQueries : BaseQueries, IVehicleQueries
    {
        private const string VehicleCodeFormat = "V{0}";
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `vehicle`");
            if (previousCode == null)
            {
                code = VehicleCodeFormat.Replace("{0}", 1.ToString("000000000"));
            }
            else
            {
                code = VehicleCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(1, 9)) + 1).ToString("000000000"));
            }

            return code;
        }

        public async Task<VehicleViewModel> Get(int id)
        {
            VehicleViewModel result = null;
            string cmd = $@"SELECT * FROM `vehicle` v
                            LEFT JOIN `vehicle_type` t ON v.type_id = t.id AND t.is_used = 1
                            WHERE v.id = {id} and v.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Vehicle, VehicleType, VehicleViewModel>(
                    (vRs, tRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Vehicle, VehicleViewModel>(vRs);
                        }

                        if (tRs != null)
                        {
                            result.Type = tRs;
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
                    rd.Read<Vehicle, VehicleType, VehicleViewModel>(
                        (vRs, tRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Vehicle, VehicleViewModel>(vRs);
                            }

                            if (tRs != null)
                            {
                                result.Type = tRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<VehicleViewModel>> Gets(string condition = "")
        {
            List<VehicleViewModel> result = new List<VehicleViewModel>();
            string cmd = $@"SELECT * FROM `vehicle` v
                            LEFT JOIN `vehicle_type` t ON v.type_id = t.id AND t.is_used = 1
                            WHERE v.is_deleted = 0";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += $" AND {condition}";
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Vehicle, VehicleType, VehicleViewModel>(
                    (vRs, tRs) =>
                    {
                        var vehicle = result.FirstOrDefault(v => v.Id == vRs.Id);
                        if (vehicle == null)
                        {
                            vehicle = CommonHelper.Mapper<Vehicle, VehicleViewModel>(vRs);
                            result.Add(vehicle);
                        }

                        if (tRs != null)
                        {
                            vehicle.Type = tRs;
                        }

                        return vehicle;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Vehicle, VehicleType, VehicleViewModel>(
                        (vRs, tRs) =>
                        {
                            var vehicle = result.FirstOrDefault(v => v.Id == vRs.Id);
                            if (vehicle == null)
                            {
                                vehicle = CommonHelper.Mapper<Vehicle, VehicleViewModel>(vRs);
                                result.Add(vehicle);
                            }

                            if (tRs != null)
                            {
                                vehicle.Type = tRs;
                            }

                            return vehicle;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<VehicleType> GetType(int id)
        {
            return (await DALHelper.ExecuteQuery<VehicleType>($"SELECT * FROM `vehicle_type` WHERE id = {id} and is_used = 1", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<VehicleType>> GetTypes(string condition = "")
        {
            string cmd = "SELECT * FROM `vehicle_type` WHERE is_used = 1";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += $" WHERE " + condition;
            }
            return await DALHelper.ExecuteQuery<VehicleType>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
