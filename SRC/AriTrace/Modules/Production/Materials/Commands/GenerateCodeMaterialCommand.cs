using Common.Exceptions;
using MDM.UI.Companies.Interfaces;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.Materials.Commands
{
    public class GenerateCodeMaterialCommand : BaseCommand<string>
    {
    }

    public class GenerateCodeMaterialCommandHandler : BaseCommandHandler<GenerateCodeMaterialCommand, string>
    {
        private readonly IMaterialService _materialService = null;

        private readonly ICompanyRepository _companyRepository = null;
        private readonly ICompanyQueries _companyQueries = null;

        public GenerateCodeMaterialCommandHandler(IMaterialService materialService, ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this._materialService = materialService;

            this._companyRepository = companyRepository;
            this._companyQueries = companyQueries;
        }

        public override async Task<string> HandleCommand(GenerateCodeMaterialCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = await _materialService.GenerateCodeAsync(company.Id);

            if (rs == null)
                throw new BusinessException("Material.NotExist");

            return rs;
        }
    }
}
