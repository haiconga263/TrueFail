using Common.Exceptions;
using DAL;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Models;
using MDM.UI.Companies.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Partner.AccountPartners.Commands
{
    public class DeleteAccountPartnerCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteAccountPartnerCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteAccountPartnerCommandHandler : BaseCommandHandler<DeleteAccountPartnerCommand, int>
    {
        private readonly IAccountRepository _accountRepository = null;
        private readonly IAccountQueries _accountQueries = null;

        private readonly ICompanyQueries _companyQueries = null;
        public DeleteAccountPartnerCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries, ICompanyQueries companyQueries)
        {
            this._accountRepository = accountRepository;
            this._accountQueries = accountQueries;

            this._companyQueries = companyQueries;
        }
        public override async Task<int> HandleCommand(DeleteAccountPartnerCommand request, CancellationToken cancellationToken)
        {

            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            Account account = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Account.NotExisted");
            }
            else
            {
                account = await _accountQueries.GetByIdAsync(request.Model);
                if (account == null)
                {
                    throw new BusinessException("Account.NotExisted");
                }
                else if (account.PartnerId != company.Id)
                {
                    throw new BusinessException("Common.NoPermission");
                }
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
