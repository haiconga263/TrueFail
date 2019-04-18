using Common.Exceptions;
using Common.Extensions;
using DAL;
using MDM.UI.AccountRoles.Interfaces;
using MDM.UI.AccountRoles.Models;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Models;
using MDM.UI.Accounts.ViewModels;
using MDM.UI.Roles.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.AccountCommands
{
    public class InsertAccountCommand : BaseCommand<int>
    {
        public AccountSingleRole Model { set; get; }
        public InsertAccountCommand(AccountSingleRole account)
        {
            Model = account;
        }
    }

    public class InsertAccountCommandHandler : BaseCommandHandler<InsertAccountCommand, int>
    {
        private readonly IAccountRepository accountRepository = null;
        private readonly IAccountQueries accountQueries = null;

        private readonly IAccountRoleRepository accountRoleRepository = null;
        private readonly IAccountRoleQueries accountRoleQueries = null;

        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;

        public InsertAccountCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries,
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
        public override async Task<int> HandleCommand(InsertAccountCommand request, CancellationToken cancellationToken)
        {
            var id = 0;

            if ((await roleQueries.GetByIdAsync(request.Model.RoleId ?? 0)) == null)
            {
                throw new BusinessException("Role.NotSelected");
            }

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        request.Model.SecurityPassword = Guid.NewGuid();
                        request.Model.Password = (request.Model.NewPassword + request.Model.SecurityPassword.ToString()).CalculateMD5Hash();

                        id = await accountRepository.AddAsync((Account)request.Model);

                        if (id > 0)
                            await accountRoleRepository.AddAsync(new AccountRole()
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
