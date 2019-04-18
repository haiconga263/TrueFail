using Common.Exceptions;
using DAL;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.AccountCommands
{
    public class DeleteAccountCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteAccountCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteAccountCommandHandler : BaseCommandHandler<DeleteAccountCommand, int>
    {
        private readonly IAccountRepository accountRepository = null;
        private readonly IAccountQueries accountQueries = null;
        public DeleteAccountCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries)
        {
            this.accountRepository = accountRepository;
            this.accountQueries = accountQueries;
        }
        public override async Task<int> HandleCommand(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Account.NotExisted");
            }
            else
            {
                account = await accountQueries.GetByIdAsync(request.Model);
                if (account == null)
                    throw new BusinessException("Account.NotExisted");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        account.IsDeleted = true;
                        account.ModifiedDate = DateTime.Now;
                        account.ModifiedBy = request.LoginSession.Id;

                        if (await accountRepository.UpdateAsync(account) > 0)
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
