using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Distributions.UI;
using Distributions.UI.Interfaces;
using Distributions.UI.Models;
using Distributions.UI.ViewModels;
using MDM.UI.Employees.Models;
using MDM.UI.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.Queries
{
    public class TripQueries : BaseQueries, ITripQueries
    {
        private const string OrderCodeFormat = "T{y}{n}"; // O<year><number>
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `trip`");
            if (previousCode == null)
            {
                code = OrderCodeFormat.Replace("{y}", DateTime.Now.Year.ToString("0000")).Replace("{n}", 1.ToString("00000"));
            }
            else
            {
                if (DateTime.Now.Year.ToString("0000").Equals(previousCode.Substring(1, 4)))
                {
                    code = previousCode.Substring(0, 5) + (Int32.Parse(previousCode.Substring(5, 5)) + 1).ToString("00000");
                }
            }

            return code;
        }

        public async Task<TripViewModel> Get(int id)
        {
            TripViewModel result = null;
            string cmd = $@"SELECT * FROM `trip` t
                            LEFT JOIN `trip_status` ts ON ts.id = t.status_id
                            LEFT JOIN `router` r ON r.id = t.router_id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `employee` dm ON dm.id = t.deliveryman_id AND dm.is_used = 1 AND dm.is_deleted = 0
                            LEFT JOIN `employee` d ON d.id = t.driver_id AND d.is_used = 1 AND d.is_deleted = 0
                            LEFT JOIN `vehicle` v ON v.id = t.vehicle_id AND v.is_used = 1 AND v.is_deleted = 0
                            WHERE t.is_deleted = 0 and t.id = {id}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                    (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                        }

                        if (tsRs != null)
                        {
                            result.Status = tsRs;
                        }

                        if (rRs != null)
                        {
                            result.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                        }

                        if (dmRs != null)
                        {
                            result.DeliveryMan = dmRs;
                        }

                        if (dRs != null)
                        {
                            result.Driver = dRs;
                        }

                        if (vRs != null)
                        {
                            result.Vehicle = vRs;
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
                    rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                        (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                            }

                            if (tsRs != null)
                            {
                                result.Status = tsRs;
                            }

                            if (rRs != null)
                            {
                                result.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                            }

                            if (dmRs != null)
                            {
                                result.DeliveryMan = dmRs;
                            }

                            if (dRs != null)
                            {
                                result.Driver = dRs;
                            }

                            if (vRs != null)
                            {
                                result.Vehicle = vRs;
                            }

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<TripViewModel>> GetByDeliveryMan(int id)
        {
            List<TripViewModel> result = new List<TripViewModel>();
            string cmd = $@"SELECT * FROM `trip` t
                            LEFT JOIN `trip_status` ts ON ts.id = t.status_id
                            LEFT JOIN `router` r ON r.id = t.router_id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `employee` dm ON dm.id = t.deliveryman_id AND dm.is_used = 1 AND dm.is_deleted = 0
                            LEFT JOIN `employee` d ON d.id = t.driver_id AND d.is_used = 1 AND d.is_deleted = 0
                            LEFT JOIN `vehicle` v ON v.id = t.vehicle_id AND v.is_used = 1 AND v.is_deleted = 0
                            WHERE t.is_deleted = 0 and t.status_id <> {(int)TripStatuses.Canceled} and t.status_id <> {(int)TripStatuses.Finished}
                                  and t.deliveryman_id = {id} ORDER BY t.delivery_date, t.modified_date";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                    (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                    {
                        var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                        if (trip == null)
                        {
                            trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                            result.Add(trip);
                        }

                        if(tsRs != null)
                        {
                            trip.Status = tsRs;
                        }

                        if(rRs != null)
                        {
                            trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                        }

                        if(dmRs != null)
                        {
                            trip.DeliveryMan = dmRs;
                        }

                        if(dRs != null)
                        {
                            trip.Driver = dRs;
                        }

                        if(vRs != null)
                        {
                            trip.Vehicle = vRs;
                        }

                        return trip;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                        (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                        {
                            var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                            if (trip == null)
                            {
                                trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                                result.Add(trip);
                            }

                            if (tsRs != null)
                            {
                                trip.Status = tsRs;
                            }

                            if (rRs != null)
                            {
                                trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                            }

                            if (dmRs != null)
                            {
                                trip.DeliveryMan = dmRs;
                            }

                            if (dRs != null)
                            {
                                trip.Driver = dRs;
                            }

                            if (vRs != null)
                            {
                                trip.Vehicle = vRs;
                            }

                            return trip;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<TripViewModel>> GetByDriver(int id)
        {
            List<TripViewModel> result = new List<TripViewModel>();
            string cmd = $@"SELECT * FROM `trip` t
                            LEFT JOIN `trip_status` ts ON ts.id = t.status_id
                            LEFT JOIN `router` r ON r.id = t.router_id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `employee` dm ON dm.id = t.deliveryman_id AND dm.is_used = 1 AND dm.is_deleted = 0
                            LEFT JOIN `employee` d ON d.id = t.driver_id AND d.is_used = 1 AND d.is_deleted = 0
                            LEFT JOIN `vehicle` v ON v.id = t.vehicle_id AND v.is_used = 1 AND v.is_deleted = 0
                            WHERE t.is_deleted = 0 and t.status_id <> {(int)TripStatuses.Canceled} and t.status_id <> {(int)TripStatuses.Finished}
                                  and t.driver_id = {id}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                    (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                    {
                        var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                        if (trip == null)
                        {
                            trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                            result.Add(trip);
                        }

                        if (tsRs != null)
                        {
                            trip.Status = tsRs;
                        }

                        if (rRs != null)
                        {
                            trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                        }

                        if (dmRs != null)
                        {
                            trip.DeliveryMan = dmRs;
                        }

                        if (dRs != null)
                        {
                            trip.Driver = dRs;
                        }

                        if (vRs != null)
                        {
                            trip.Vehicle = vRs;
                        }

                        return trip;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                        (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                        {
                            var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                            if (trip == null)
                            {
                                trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                                result.Add(trip);
                            }

                            if (tsRs != null)
                            {
                                trip.Status = tsRs;
                            }

                            if (rRs != null)
                            {
                                trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                            }

                            if (dmRs != null)
                            {
                                trip.DeliveryMan = dmRs;
                            }

                            if (dRs != null)
                            {
                                trip.Driver = dRs;
                            }

                            if (vRs != null)
                            {
                                trip.Vehicle = vRs;
                            }

                            return trip;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<TripViewModel>> Gets(string conditions)
        {
            List<TripViewModel> result = new List<TripViewModel>();
            string cmd = $@"SELECT * FROM `trip` t
                            LEFT JOIN `trip_status` ts ON ts.id = t.status_id
                            LEFT JOIN `router` r ON r.id = t.router_id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `employee` dm ON dm.id = t.deliveryman_id AND dm.is_used = 1 AND dm.is_deleted = 0
                            LEFT JOIN `employee` d ON d.id = t.driver_id AND d.is_used = 1 AND d.is_deleted = 0
                            LEFT JOIN `vehicle` v ON v.id = t.vehicle_id AND v.is_used = 1 AND v.is_deleted = 0
                            WHERE t.is_deleted = 0 and t.status_id <> {(int)TripStatuses.Canceled} and t.status_id <> {(int)TripStatuses.Finished}";
            if(!string.IsNullOrEmpty(conditions))
            {
                cmd += $" and {conditions}";
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                    (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                    {
                        var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                        if (trip == null)
                        {
                            trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                            result.Add(trip);
                        }

                        if (tsRs != null)
                        {
                            trip.Status = tsRs;
                        }

                        if (rRs != null)
                        {
                            trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                        }

                        if (dmRs != null)
                        {
                            trip.DeliveryMan = dmRs;
                        }

                        if (dRs != null)
                        {
                            trip.Driver = dRs;
                        }

                        if (vRs != null)
                        {
                            trip.Vehicle = vRs;
                        }

                        return trip;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                        (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                        {
                            var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                            if (trip == null)
                            {
                                trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                                result.Add(trip);
                            }

                            if (tsRs != null)
                            {
                                trip.Status = tsRs;
                            }

                            if (rRs != null)
                            {
                                trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                            }

                            if (dmRs != null)
                            {
                                trip.DeliveryMan = dmRs;
                            }

                            if (dRs != null)
                            {
                                trip.Driver = dRs;
                            }

                            if (vRs != null)
                            {
                                trip.Vehicle = vRs;
                            }

                            return trip;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<TripViewModel>> GetHistorys(string conditions)
        {
            List<TripViewModel> result = new List<TripViewModel>();
            string cmd = $@"SELECT * FROM `trip` t
                            LEFT JOIN `trip_status` ts ON ts.id = t.status_id
                            LEFT JOIN `router` r ON r.id = t.router_id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `employee` dm ON dm.id = t.deliveryman_id AND dm.is_used = 1 AND dm.is_deleted = 0
                            LEFT JOIN `employee` d ON d.id = t.driver_id AND d.is_used = 1 AND d.is_deleted = 0
                            LEFT JOIN `vehicle` v ON v.id = t.vehicle_id AND v.is_used = 1 AND v.is_deleted = 0
                            WHERE t.is_deleted = 0 and (t.status_id = {(int)TripStatuses.Canceled} or t.status_id = {(int)TripStatuses.Finished})";
            if (!string.IsNullOrEmpty(conditions))
            {
                cmd += $" and {conditions}";
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                    (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                    {
                        var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                        if (trip == null)
                        {
                            trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                            result.Add(trip);
                        }

                        if (tsRs != null)
                        {
                            trip.Status = tsRs;
                        }

                        if (rRs != null)
                        {
                            trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                        }

                        if (dmRs != null)
                        {
                            trip.DeliveryMan = dmRs;
                        }

                        if (dRs != null)
                        {
                            trip.Driver = dRs;
                        }

                        if (vRs != null)
                        {
                            trip.Vehicle = vRs;
                        }

                        return trip;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Trip, TripStatus, Router, Employee, Employee, Vehicle, TripViewModel>(
                        (tRs, tsRs, rRs, dmRs, dRs, vRs) =>
                        {
                            var trip = result.FirstOrDefault(t => t.Id == tRs.Id);
                            if (trip == null)
                            {
                                trip = CommonHelper.Mapper<Trip, TripViewModel>(tRs);
                                result.Add(trip);
                            }

                            if (tsRs != null)
                            {
                                trip.Status = tsRs;
                            }

                            if (rRs != null)
                            {
                                trip.Router = CommonHelper.Mapper<Router, RouterViewModel>(rRs);
                            }

                            if (dmRs != null)
                            {
                                trip.DeliveryMan = dmRs;
                            }

                            if (dRs != null)
                            {
                                trip.Driver = dRs;
                            }

                            if (vRs != null)
                            {
                                trip.Vehicle = vRs;
                            }

                            return trip;
                        }
                    );

                    return result;
                }
            }
        }

        public Task<IEnumerable<TripStatus>> GetStatuses()
        {
            return DALHelper.ExecuteQuery<TripStatus>("SELECT * FROM trip_status", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
