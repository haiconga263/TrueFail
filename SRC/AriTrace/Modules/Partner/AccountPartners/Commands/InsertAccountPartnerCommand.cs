using Common.Exceptions;
using Common.Extensions;
using Common.Helpers;
using DAL;
using MDM.UI.AccountRoles.Interfaces;
using MDM.UI.AccountRoles.Models;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Models;
using MDM.UI.Accounts.ViewModels;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Roles.Interfaces;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Partner.AccountPartners.Commands
{
    public class InsertAccountPartnerCommand : BaseCommand<int>
    {
        public AccountSingleRole Model { set; get; }
        public InsertAccountPartnerCommand(AccountSingleRole account)
        {
            Model = account;
        }
    }

    public class InsertAccountPartnerCommandHandler : BaseCommandHandler<InsertAccountPartnerCommand, int>
    {
        private readonly IAccountRepository _accountRepository = null;
        private readonly IAccountQueries _accountQueries = null;

        private readonly IAccountRoleRepository _accountRoleRepository = null;
        private readonly IAccountRoleQueries _accountRoleQueries = null;

        private readonly IRoleRepository _roleRepository = null;
        private readonly IRoleQueries _roleQueries = null;

        private readonly ICompanyQueries _companyQueries = null;
        private readonly ISettingQueries _settingQueries = null;

        public InsertAccountPartnerCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries,
                                            IAccountRoleRepository accountRoleRepository, IAccountRoleQueries accountRoleQueries,
                                            IRoleRepository roleRepository, IRoleQueries roleQueries,
                                            ICompanyQueries _companyQueries,
                                            ISettingQueries settingQueries)
        {
            this._accountRepository = accountRepository;
            this._accountQueries = accountQueries;

            this._accountRoleRepository = accountRoleRepository;
            this._accountRoleQueries = accountRoleQueries;

            this._roleRepository = roleRepository;
            this._roleQueries = roleQueries;

            this._companyQueries = _companyQueries;
            this._settingQueries = settingQueries;

        }
        public override async Task<int> HandleCommand(InsertAccountPartnerCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var id = 0;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.RoleId = (await _settingQueries.GetValueAsync(SettingKeys.Role_Default)).ToInt();
                        if ((await _roleQueries.GetByIdAsync(request.Model.RoleId ?? 0)) == null)
                        {
                            throw new BusinessException("Role.NotSelected");
                        }

                        request.Model.PartnerId = company.Id;
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        request.Model.SecurityPassword = Guid.NewGuid();
                        request.Model.Password = (request.Model.NewPassword + request.Model.SecurityPassword.ToString()).CalculateMD5Hash();

                        id = await _accountRepository.AddAsync((Account)request.Model);

                        if (id > 0)
                            await _accountRoleRepository.AddAsync(new AccountRole()
                            {
                                RoleId = request.Model.RoleId ?? 0,
                                UserAccountId = id,
                                CreatedDate = DateTime.Now,
                            });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (id > 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return id;
        }
    }
}
