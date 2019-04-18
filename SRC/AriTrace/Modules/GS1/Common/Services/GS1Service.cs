using GS1.UI.Common.Interfaces;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Productions.Interfaces;
using GS1.UI.SessionBuffers.Interfaces;
using MDM.UI.Companies.Interfaces;

namespace GS1.Common.Services
{
    public class GS1Service : IGS1Service
    {
        private readonly IProductionQueries productionQueries;
        private readonly IProductionRepository productionRepository;

        private readonly ICompanyQueries companyQueries;
        private readonly ICompanyRepository companyRepository;

        private readonly IGTINQueries gTINQueries;
        private readonly IGTINRepository gTINRepository;

        private readonly ISessionBufferQueries sessionBufferQueries;
        private readonly ISessionBufferRepository sessionBufferRepository;

        public GS1Service(IProductionQueries productionQueries, IProductionRepository productionRepository,
                        ICompanyQueries companyQueries, ICompanyRepository companyRepository,
                        IGTINQueries gTINQueries, IGTINRepository gTINRepository,
                        ISessionBufferQueries sessionBufferQueries, ISessionBufferRepository sessionBufferRepository)
        {
            this.productionQueries = productionQueries;
            this.productionRepository = productionRepository;

            this.companyQueries = companyQueries;
            this.companyRepository = companyRepository;

            this.gTINQueries = gTINQueries;
            this.gTINRepository = gTINRepository;

            this.sessionBufferQueries = sessionBufferQueries;
            this.sessionBufferRepository = sessionBufferRepository;
        }

    }
}
