using Common.Exceptions;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.ViewModels;
using MDM.UI.Companies.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Partner.AccountPartners.Commands
{
    public class GetByIdAccountPartnerCommand : BaseCommand<AccountSingleRole>
    {
        public int Model { get; set; }

        public GetByIdAccountPartnerCommand(int id)
        {
            Model = id;
        }
    }

    public class GetByIdAccountPartnerCommandHandler : BaseCommandHandler<GetByIdAccountPartnerCommand, AccountSingleRole>
    {
        private readonly IAccountRepository _accountRepository = null;
        private readonly IAccountQueries accountQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        public GetByIdAccountPartnerCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this._accountRepository = accountRepository;
            this.accountQueries = accountQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;
        }

        public override async Task<AccountSingleRole> HandleCommand(GetByIdAccountPartnerCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = await accountQueries.GetByIdAsync(request.Model);

            if (rs == null)
                throw new BusinessException("Account.NotExist");
 
            return rs;
        }
    }
}
