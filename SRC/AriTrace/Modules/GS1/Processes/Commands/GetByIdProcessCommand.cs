using Common.Exceptions;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Processes.Interfaces;
using GS1.UI.Processes.Models;
using GS1.UI.Processes.ViewModels;
using MDM.UI.Companies.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Processes.Commands
{
    public class GetByIdProcessCommand : BaseCommand<ProcessInformation>
    {
        public int Model { get; set; }

        public GetByIdProcessCommand(int id)
        {
            Model = id;
        }
    }

    public class GetByIdProcessCommandHandler : BaseCommandHandler<GetByIdProcessCommand, ProcessInformation>
    {
        private readonly IProcessRepository processRepository = null;
        private readonly IProcessQueries processQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public GetByIdProcessCommandHandler(IProcessRepository processRepository, IProcessQueries processQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.processRepository = processRepository;
            this.processQueries = processQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }

        public override async Task<ProcessInformation> HandleCommand(GetByIdProcessCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = await processQueries.GetByIdAsync(request.Model);

            if (rs == null)
                throw new BusinessException("Process.NotExist");
            if (rs.Production?.GTIN != null)
                rs.Production.GTIN.Code = await gTINService.GetCodeGTINAsync(rs.Production.GTIN);
            return rs;
        }
    }
}
