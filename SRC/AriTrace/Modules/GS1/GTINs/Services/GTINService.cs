using Common.Models;
using GS1.GTINs.Factories;
using GS1.UI.Buffers.Enumerations;
using GS1.UI.GTINs.Enumerations;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Models;
using GS1.UI.Productions.Interfaces;
using GS1.UI.SessionBuffers.Interfaces;
using GS1.UI.SessionBuffers.Models;
using MDM.UI.Companies.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace GS1.GTINs.Services
{
    public class GTINService : BaseService, IGTINService
    {
        private readonly IProductionQueries productionQueries;
        private readonly IProductionRepository productionRepository;

        private readonly ICompanyQueries companyQueries;
        private readonly ICompanyRepository companyRepository;

        private readonly IGTINQueries gTINQueries;
        private readonly IGTINRepository gTINRepository;

        private readonly ISessionBufferService _sessionBufferService;

        public GTINService(IProductionQueries productionQueries, IProductionRepository productionRepository,
                        ICompanyQueries companyQueries, ICompanyRepository companyRepository,
                        IGTINQueries gTINQueries, IGTINRepository gTINRepository,
                        ISessionBufferService sessionBufferService)
        {
            this.productionQueries = productionQueries;
            this.productionRepository = productionRepository;

            this.companyQueries = companyQueries;
            this.companyRepository = companyRepository;

            this.gTINQueries = gTINQueries;
            this.gTINRepository = gTINRepository;

            this._sessionBufferService = sessionBufferService;
        }

        public async Task<GTIN> GenerateGTINAsync(int partnerId, GTINTypes gTINTypes, UserSession session)
        {
            var company = await companyQueries.GetByIdAsync(partnerId);
            var productions = (await productionQueries.GetAllAsync(company.Id)).ToList();

            var gTINId = productions.Select(x => x.GTINId);
            var gTINs = await gTINQueries.GetByCompanyCodeAsync(company.GS1Code);
            long maxNumericExist = (gTINs.Max(x => (long?)x.Numeric) ?? 0) + 1;

            var sessionBuffers = await _sessionBufferService.GetByCompanyIdAsync(company.Id, SessionBufferTypes.GTIN);
            var now = DateTime.Now;
            long maxNumericBuffer = sessionBuffers
                .Where(x => x.ExpiredDate > now && x.PartnerId == partnerId)
                .Select(x => JsonConvert.DeserializeObject<GTIN>(x.DataJson)?.Numeric)
                .Max() ?? 0;

            long numericGenerate = (maxNumericExist > maxNumericBuffer ? maxNumericExist : maxNumericBuffer) + 1;
            GTIN gTIN = new GTIN()
            {
                CompanyCode = company.GS1Code,
                Numeric = numericGenerate,
                PartnerId = partnerId,
                Type = gTINTypes,
                UsedDate = DateTime.Now,
                IsUsed = true,
                ModifiedBy = session.Id,
                ModifiedDate = DateTime.Now,
                CreatedBy = session.Id,
                CreatedDate = DateTime.Now
            };

            var id = await _sessionBufferService.InsertOrUpdateAsync(new SessionBuffer()
            {
                PartnerId = company.Id,
                SessionId = session.SessionId,
                DataJson = JsonConvert.SerializeObject(gTIN),
                ExpiredDate = DateTime.Now.AddHours(2),
                Type = SessionBufferTypes.GTIN,
            }, session);

            return gTIN;
        }

        public async Task<GTINValidationCodes> CheckNewGTINAsync(int partnerId, GTIN gTIN, UserSession session)
        {
            var company = await companyQueries.GetByIdAsync(partnerId);

            var gTINService = gTIN.Type.GetGTINService(gTINQueries, gTINRepository, _sessionBufferService);
            return await gTINService.CheckNewGTINAsync(company, gTIN, session);
        }

        public async Task<string> GetCodeGTINAsync(GTIN gTIN)
        {
            var gTINService = gTIN.Type.GetGTINService(gTINQueries, gTINRepository, _sessionBufferService);
            var rs = gTINService.ValidationFormat(gTIN);
            if (rs != GTINValidationCodes.GTINValid && rs != GTINValidationCodes.CheckDigitInValid)
                throw rs.GetException();

            return gTINService.GetCode(gTIN, rs == GTINValidationCodes.CheckDigitInValid);
        }

        public async Task<GTIN> CalculateCheckDigitAsync(GTIN gTIN)
        {
            var gTINService = gTIN.Type.GetGTINService(gTINQueries, gTINRepository, _sessionBufferService);
            var rs = gTINService.ValidationFormat(gTIN);
            if (rs != GTINValidationCodes.GTINValid && rs != GTINValidationCodes.CheckDigitInValid)
                throw rs.GetException();

            return gTINService.CalculateCheckDigit(gTIN);
        }

        public async Task<GTIN> InsertOrUpdateGTINAsync(int partnerId, GTIN gTIN, UserSession session)
        {
            gTIN.PartnerId = partnerId;
            var gTINService = gTIN.Type.GetGTINService(gTINQueries, gTINRepository, _sessionBufferService);
            var rs = gTINService.ValidationFormat(gTIN);
            if (rs != GTINValidationCodes.GTINValid && rs != GTINValidationCodes.CheckDigitInValid)
                throw rs.GetException();
            if (rs == GTINValidationCodes.CheckDigitInValid)
                gTIN = gTINService.CalculateCheckDigit(gTIN);

            int id = gTIN.Id;
            gTIN.ModifiedBy = session.Id;
            gTIN.ModifiedDate = DateTime.Now;
            if (gTIN.Id <= 0)
            {
                gTIN.CreatedBy = session.Id;
                gTIN.CreatedDate = DateTime.Now;
                gTIN.UsedDate = DateTime.Now;
                id = await gTINRepository.AddAsync(gTIN);
            }
            else await gTINRepository.UpdateAsync(gTIN);

            return await gTINQueries.GetByIdAsync(id);
        }
    }
}
