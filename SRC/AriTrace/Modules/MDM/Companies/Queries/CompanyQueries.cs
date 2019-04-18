using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Addresses.Models;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Companies.Models;
using MDM.UI.Companies.ViewModels;
using MDM.UI.CompanyTypes.Models;
using MDM.UI.Contacts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Companies.Queries
{
    public class CompanyQueries : BaseQueries, ICompanyQueries
    {
        public async Task<IEnumerable<CompanyViewModel>> GetAllAsync()
        {
            string cmd = $@"SELECT c.*, addr.*, ctt.*, ct.* FROM `company` c
                            LEFT JOIN `address` addr ON c.address_id = addr.id
                            LEFT JOIN `contact` ctt ON c.contact_id = ctt.id
                            LEFT JOIN `company_type` ct ON c.company_type_id = ct.id
                            WHERE c.`is_deleted` = 0";
            var conn = DbConnection;

            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Company, Address, Contact, CompanyType, CompanyViewModel>(
                        (companyRs, addressRs, contactRs, companyTypeRs) =>
                        {
                            CompanyViewModel company = null;
                            if (companyRs != null)
                            {
                                var serializedParent = JsonConvert.SerializeObject(companyRs);
                                company = JsonConvert.DeserializeObject<CompanyViewModel>(serializedParent);
                            }
                            else
                            {
                                company = new CompanyViewModel();
                            }
                            if (addressRs != null)
                            {
                                company.Address = addressRs;
                            }
                            if (contactRs != null)
                            {
                                company.Contact = contactRs;
                            }
                            if (companyTypeRs != null)
                            {
                                company.CompanyType = companyTypeRs;
                            }
                            return company;
                        }
                    );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<CompanyViewModel> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT c.*, addr.*, ctt.*, ct.* FROM `company` c
                            LEFT JOIN `address` addr ON c.address_id = addr.id
                            LEFT JOIN `contact` ctt ON c.contact_id = ctt.id
                            LEFT JOIN `company_type` ct ON c.company_type_id = ct.id
                            WHERE c.`id` = '{id}' and c.`is_deleted` = 0";
            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Company, Address, Contact, CompanyType, CompanyViewModel>(
                           (companyRs, addressRs, contactRs, companyTypeRs) =>
                           {
                               CompanyViewModel company = null;
                               if (companyRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(companyRs);
                                   company = JsonConvert.DeserializeObject<CompanyViewModel>(serializedParent);
                               }
                               else
                               {
                                   company = new CompanyViewModel();
                               }
                               if (addressRs != null)
                               {
                                   company.Address = addressRs;
                               }
                               if (contactRs != null)
                               {
                                   company.Contact = contactRs;
                               }
                               if (companyTypeRs != null)
                               {
                                   company.CompanyType = companyTypeRs;
                               }
                               return company;
                           }
                       ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<CompanyViewModel> GetByUserIdAsync(int id)
        {
            string cmd = $@"SELECT c.*, addr.*, ctt.*, ct.* FROM `company` c
		                        LEFT JOIN `user_account` ua ON ua.partner_id = c.id
		                        LEFT JOIN `address` addr ON c.address_id = addr.id
		                        LEFT JOIN `contact` ctt ON c.contact_id = ctt.id
		                        LEFT JOIN `company_type` ct ON c.company_type_id = ct.id
		                        WHERE ua.`id` = '{id}' AND c.`id` > 0 AND c.`is_deleted` = 0";
            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Company, Address, Contact, CompanyType, CompanyViewModel>(
                           (companyRs, addressRs, contactRs, companyTypeRs) =>
                           {
                               CompanyViewModel company = null;
                               if (companyRs != null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(companyRs);
                                   company = JsonConvert.DeserializeObject<CompanyViewModel>(serializedParent);
                               }
                               else
                               {
                                   company = new CompanyViewModel();
                               }
                               if (addressRs != null)
                               {
                                   company.Address = addressRs;
                               }
                               if (contactRs != null)
                               {
                                   company.Contact = contactRs;
                               }
                               if (companyTypeRs != null)
                               {
                                   company.CompanyType = companyTypeRs;
                               }
                               return company;
                           }
                       ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<IEnumerable<CompanyViewModel>> GetsAsync()
        {
            string cmd = $@"SELECT c.*, addr.*, ctt.*, ct.* FROM `company` c
                            LEFT JOIN `address` addr ON c.address_id = addr.id
                            LEFT JOIN `contact` ctt ON c.contact_id = ctt.id
                            LEFT JOIN `company_type` ct ON c.company_type_id = ct.id
                            WHERE c.`is_used` = 1 AND c.`is_deleted` = 0";
            if (DbConnection == null)
            {
                using (var conn = DALHelper.GetConnection())
                {
                    using (var reader = await conn.QueryMultipleAsync(cmd))
                    {
                        return reader.Read<Company, Address, Contact, CompanyType, CompanyViewModel>(
                            (companyRs, addressRs, contactRs, companyTypeRs) =>
                            {
                                CompanyViewModel company = null;
                                if (companyRs != null)
                                {
                                    var serializedParent = JsonConvert.SerializeObject(companyRs);
                                    company = JsonConvert.DeserializeObject<CompanyViewModel>(serializedParent);
                                }
                                else
                                {
                                    company = new CompanyViewModel();
                                }
                                if (addressRs != null)
                                {
                                    company.Address = addressRs;
                                }
                                if (contactRs != null)
                                {
                                    company.Contact = contactRs;
                                }
                                if (companyTypeRs != null)
                                {
                                    company.CompanyType = companyTypeRs;
                                }
                                return company;
                            }
                        );
                    }
                }
            }
            else
            {
                using (var reader = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<CompanyViewModel, Address, Contact, CompanyType, CompanyViewModel>(
                        (companyRs, addressRs, contactRs, companyTypeRs) =>
                        {
                            CompanyViewModel company = null;
                            if (companyRs != null)
                            {
                                company = companyRs;
                            }
                            else
                            {
                                company = new CompanyViewModel();
                            }
                            if (addressRs != null)
                            {
                                company.Address = addressRs;
                            }
                            if (contactRs != null)
                            {
                                company.Contact = contactRs;
                            }
                            if (companyTypeRs != null)
                            {
                                company.CompanyType = companyTypeRs;
                            }
                            return company;
                        }
                    );
                }
            }
        }

    }
}
