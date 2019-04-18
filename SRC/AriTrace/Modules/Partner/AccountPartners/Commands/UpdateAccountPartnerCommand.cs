using Common.Exceptions;
using DAL;
using MDM.UI.AccountRoles.Interfaces;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Models;
using MDM.UI.Accounts.ViewModels;
using MDM.UI.Roles.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using MDM.UI.AccountRoles.Models;
using Common.Extensions;
using MDM.UI.Companies.Interfaces;

namespace Partner.AccountPartners.Commands
{
    public class UpdateAccountPartnerCommand : BaseCommand<int>
    {
        public AccountSingleRole Model { set; get; }
        public UpdateAccountPartnerCommand(AccountSingleRole account)
        {
            Model = account;
        }
    }

    public class UpdateAccountPartnerCommandHandler : BaseCommandHandler<UpdateAccountPartnerCommand, int>
    {
        private readonly IAccountRepository _accountRepository = null;
        private readonly IAccountQueries _accountQueries = null;

        private readonly IAccountRoleRepository _accountRoleRepository = null;
        private readonly IAccountRoleQueries _accountRoleQueries = null;

        private readonly IRoleRepository _roleRepository = null;
        private readonly IRoleQueries _roleQueries = null;

        private readonly ICompanyQueries _companyQueries = null;

        public UpdateAccountPartnerCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries,
                                            IAccountRoleRepository accountRoleRepository, IAccountRoleQueries accountRoleQueries,
                                            IRoleRepository roleRepository, IRoleQueries roleQueries,
                                            ICompanyQueries companyQueries)
        {
            this._accountRepository = accountRepository;
            this._accountQueries = accountQueries;

            this._accountRoleRepository = accountRoleRepository;
            this._accountRoleQueries = accountRoleQueries;

            this._roleRepository = roleRepository;
            this._roleQueries = roleQueries;

            this._companyQueries = companyQueries;
        }
        public override async Task<int> HandleCommand(UpdateAccountPartnerCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            AccountSingleRole account = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Account.NotExisted");
            }
            else
            {
                account = await _accountQueries.GetByIdAsync(request.Model.Id);
                if (account == null)
                {
                    throw new BusinessException("Account.NotExisted");
                }
                else if (account.PartnerId != company.Id)
                {
                    throw new BusinessException("Common.NoPermission");
                }
            }

            if ((await _roleQueries.GetByIdAsync(request.Model.RoleId ?? 0)) == null)
            {
                throw new BusinessException("Role.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        account.UserName = request.Model.UserName;
                        account.Email = request.Model.Email;
                        account.IsActived = request.Model.IsActived;
                        account.IsExternalUser = request.Model.IsExternalUser;
                        account.IsUsed = request.Model.IsUsed;

                        account.ModifiedDate = DateTime.Now;
                        account.ModifiedBy = request.LoginSession.Id;
                        if (!string.IsNullOrWhiteSpace(request.Model.NewPassword))
                            account.Password = (request.Model.NewPassword + account.SecurityPassword.ToString()).CalculateMD5Hash();


                        if (await _accountRepository.UpdateAsync(account) > 0)
                            rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }
            return rs;
        }
    }
}
