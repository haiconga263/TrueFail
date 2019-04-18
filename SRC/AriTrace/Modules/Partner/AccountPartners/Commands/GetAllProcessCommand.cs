using Common.Exceptions;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.ViewModels;
using MDM.UI.Companies.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Partner.AccountPartners.Commands
{
    public class GetAllAccountPartnerCommand : BaseCommand<IEnumerable<AccountSingleRole>>
    {
        public GetAllAccountPartnerCommand()
        {
        }
    }

    public class GetAllAccountPartnerCommandHandler : BaseCommandHandler<GetAllAccountPartnerCommand, IEnumerable<AccountSingleRole>>
    {
        private readonly IAccountRepository accountRepository = null;
        private readonly IAccountQueries accountQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        public GetAllAccountPartnerCommandHandler(IAccountRepository accountRepository, IAccountQueries accountQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this.accountRepository = accountRepository;
            this.accountQueries = accountQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;
        }

        public override async Task<IEnumerable<AccountSingleRole>> HandleCommand(GetAllAccountPartnerCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var lst = (await accountQueries.GetAllAsync(company?.Id));

            return lst;
        }
    }
}
