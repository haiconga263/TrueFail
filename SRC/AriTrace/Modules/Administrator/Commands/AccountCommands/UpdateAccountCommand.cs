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

namespace Administrator.Commands.AccountCommands
{
    public class UpdateAccountCommand : BaseCommand<int>
    {
        public AccountSingleRole Model { set; get; }
        public UpdateAccountCommand(AccountSingleRole account)
        {
            Model = account;
        }
    }

    public class UpdateAccountCommandHandler : BaseCommandHandler<UpdateAccountCommand, int>
    {
        private readonly IAccountRepository accountRepository = null;
        private readonly IAccountQueries accountQueries = null;

        private readonly IAccountRoleRepository accountRoleRepository = null;
        private readonly IAccountRoleQueries accountRoleQueries = null;

        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries,
                                            IAccountRoleRepository accountRoleRepository, IAccountRoleQueries accountRoleQueries,
                                            IRoleRepository roleRepository, IRoleQueries roleQueries)
        {
            this.accountRepository = accountRepository;
            this.accountQueries = accountQueries;

            this.accountRoleRepository = accountRoleRepository;
            this.accountRoleQueries = accountRoleQueries;

            this.roleRepository = roleRepository;
            this.roleQueries = roleQueries;
        }
        public override async Task<int> HandleCommand(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            AccountSingleRole account = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Account.NotExisted");
            }
            else
            {
                account = await accountQueries.GetByIdAsync(request.Model.Id);
                if (account == null)
                {
                    throw new BusinessException("Account.NotExisted");
                }
            }

            if ((await roleQueries.GetByIdAsync(request.Model.RoleId ?? 0)) == null)
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
                        if (account.RoleId != request.Model.RoleId)
                        {
                            var accRoles = (await accountRoleQueries.GetsAsync($"user_account_id = {request.Model.Id}")).ToList();
                            for (int i = 0; i < accRoles.Count; i++)
                            {
                                await accountRoleRepository.DeleteAsync(accRoles[i].Id);
                            }

                            await accountRoleRepository.AddAsync(new AccountRole()
                            {
                                RoleId = request.Model.RoleId ?? 0,
                                UserAccountId = account.Id,
                                CreatedDate = DateTime.Now,
                            });
                        }

                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        request.Model.IsDeleted = account.IsDeleted;
                        request.Model.SecurityPassword = account.SecurityPassword;
                        if (!string.IsNullOrWhiteSpace(request.Model.NewPassword))
                            request.Model.Password = (request.Model.NewPassword + request.Model.SecurityPassword.ToString()).CalculateMD5Hash();
                        else request.Model.Password = account.Password;

                        if (await accountRepository.UpdateAsync((Account)request.Model) > 0)
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
