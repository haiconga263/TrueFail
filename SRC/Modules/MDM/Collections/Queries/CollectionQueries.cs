using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Collections.ViewModels;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Collections.Queries
{
    public class CollectionQueries : BaseQueries, ICollectionQueries
    {
        private const string CollectionCodeFormat = "C{0}";
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `collection`");
            if (previousCode == null)
            {
                code = CollectionCodeFormat.Replace("{0}", 1.ToString("000000000"));
            }
            else
            {
                code = CollectionCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(1, 9)) + 1).ToString("000000000"));
            }

            return code;
        }
        public async Task<IEnumerable<CollectionViewModel>> GetsByEmployeeId(int employeeId)
        {
            List<CollectionViewModel> result = new List<CollectionViewModel>();
            string cmd = $@"SELECT * FROM (SELECT * FROM `collection` cc
		                            	   WHERE cc.manager_id = {employeeId} or EXISTS(SELECT Id FROM `collection_employee` ce 
		                            										 WHERE ce.collection_id = cc.Id AND ce.employee_id = {employeeId})) c
		                             LEFT JOIN `address` a ON a.id = c.address_id AND a.is_used = 1 AND a.is_deleted = 0
		                             LEFT JOIN `contact` ccc ON ccc.id = c.contact_id AND ccc.is_used = 1 AND ccc.is_deleted = 0
		                             WHERE c.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Collection, Address, Contact, CollectionViewModel>(
                    (cRs, aRs, ccRs) =>
                    {
                        var collection = result.FirstOrDefault(c => c.Id == cRs.Id);
                        if (collection == null)
                        {
                            collection = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
                            result.Add(collection);
                        }

                        if (collection.Address == null)
                        {
                            collection.Address = aRs;
                        }

                        if (collection.Contact == null)
                        {
                            collection.Contact = ccRs;
                        }

                        return collection;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Collection, Address, Contact, CollectionViewModel>(
                        (cRs, aRs, ccRs) =>
                        {
                            var collection = result.FirstOrDefault(c => c.Id == cRs.Id);
                            if (collection == null)
                            {
                                collection = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
                                result.Add(collection);
                            }

                            if (collection.Address == null)
                            {
                                collection.Address = aRs;
                            }

                            if (collection.Contact == null)
                            {
                                collection.Contact = ccRs;
                            }

                            return collection;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<CollectionViewModel>> Gets(string condition = "")
        {
            List<CollectionViewModel> result = new List<CollectionViewModel>();
            string cmd = $@"SELECT * FROM `collection` c
                            LEFT JOIN `address` a ON a.id = c.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = c.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE c.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Collection, Address, Contact, CollectionViewModel>(
                    (cRs, aRs, ccRs) =>
                    {
                        var collection = result.FirstOrDefault(c => c.Id == cRs.Id);
                        if (collection == null)
                        {
                            collection = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
                            result.Add(collection);
                        }

                        if (collection.Address == null)
                        {
                            collection.Address = aRs;
                        }

                        if (collection.Contact == null)
                        {
                            collection.Contact = ccRs;
                        }

                        return collection;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Collection, Address, Contact, CollectionViewModel>(
                        (cRs, aRs, ccRs) =>
                        {
                            var collection = result.FirstOrDefault(c => c.Id == cRs.Id);
                            if (collection == null)
                            {
                                collection = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
                                result.Add(collection);
                            }
                    
                            if (collection.Address == null)
                            {
                                collection.Address = aRs;
                            }
                    
                            if (collection.Contact == null)
                            {
                                collection.Contact = ccRs;
                            }
                    
                            return collection;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<CollectionViewModel> Get(int id)
        {
            CollectionViewModel result = null;
            string cmd = $@"SELECT * FROM `collection` c
                            LEFT JOIN `address` a ON a.id = c.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = c.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE c.id = {id} and c.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Collection, Address, Contact, CollectionViewModel>(
                    (cRs, aRs, ccRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
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
                    rd.Read<Collection, Address, Contact, CollectionViewModel>(
                        (cRs, aRs, ccRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
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

        public async Task<CollectionViewModel> GetByCode(string code)
        {
            CollectionViewModel result = null;
            string cmd = $@"SELECT * FROM `collection` c
                            LEFT JOIN `address` a ON a.id = c.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = c.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE c.code = '{code}' and c.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Collection, Address, Contact, CollectionViewModel>(
                    (cRs, aRs, ccRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
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
                    rd.Read<Collection, Address, Contact, CollectionViewModel>(
                        (cRs, aRs, ccRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
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

        public async Task<IEnumerable<CollectionViewModel>> GetsBySupervisor(int managerId)
        {
            List<CollectionViewModel> result = new List<CollectionViewModel>();
            string cmd = $@"SELECT * FROM `collection` c
                            LEFT JOIN `address` a ON a.id = c.address_id AND a.is_used = 1 AND a.is_deleted = 0
                            LEFT JOIN `contact` cc ON cc.id = c.contact_id AND cc.is_used = 1 AND cc.is_deleted = 0
                            WHERE c.is_deleted = 0 and c.manager_id = {managerId}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<Collection, Address, Contact, CollectionViewModel>(
                    (cRs, aRs, ccRs) =>
                    {
                        var collection = result.FirstOrDefault(c => c.Id == cRs.Id);
                        if (collection == null)
                        {
                            collection = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
                            result.Add(collection);
                        }

                        if (collection.Address == null)
                        {
                            collection.Address = aRs;
                        }

                        if (collection.Contact == null)
                        {
                            collection.Contact = ccRs;
                        }

                        return collection;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<Collection, Address, Contact, CollectionViewModel>(
                        (cRs, aRs, ccRs) =>
                        {
                            var collection = result.FirstOrDefault(c => c.Id == cRs.Id);
                            if (collection == null)
                            {
                                collection = CommonHelper.Mapper<Collection, CollectionViewModel>(cRs);
                                result.Add(collection);
                            }

                            if (collection.Address == null)
                            {
                                collection.Address = aRs;
                            }

                            if (collection.Contact == null)
                            {
                                collection.Contact = ccRs;
                            }

                            return collection;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
