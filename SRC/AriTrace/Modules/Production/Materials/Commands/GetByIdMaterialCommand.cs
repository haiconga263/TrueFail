using Common.Exceptions;
using MDM.UI.Companies.Interfaces;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.Materials.Commands
{
    public class GetByIdMaterialCommand : BaseCommand<Material>
    {
        public int Model { get; set; }

        public GetByIdMaterialCommand(int id)
        {
            Model = id;
        }
    }

    public class GetByIdMaterialCommandHandler : BaseCommandHandler<GetByIdMaterialCommand, Material>
    {
        private readonly IMaterialRepository _materialRepository = null;
        private readonly IMaterialQueries materialQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        public GetByIdMaterialCommandHandler(IMaterialRepository materialRepository, IMaterialQueries materialQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this._materialRepository = materialRepository;
            this.materialQueries = materialQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;
        }

        public override async Task<Material> HandleCommand(GetByIdMaterialCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = await materialQueries.GetByIdAsync(request.Model);

            if (rs == null)
                throw new BusinessException("Material.NotExist");
 
            return rs;
        }
    }
}
