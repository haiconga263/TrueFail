using Common.Models;
using GS1.GTINs.Factories;
using GS1.UI.Buffers.Enumerations;
using GS1.UI.GTINs.Enumerations;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Models;
using GS1.UI.Interfaces;
using GS1.UI.SessionBuffers.Interfaces;
using MDM.UI.Companies.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace GS1.GTINs.Services
{
    public abstract class BaseGTINTypeService : BaseService, IGTINTypeService
    {
        public abstract int MaxLengthNumeric { get; }


        protected readonly IGTINQueries _gTINQueries;
        protected readonly IGTINRepository _gTINRepository;

        protected readonly ISessionBufferService _sessionBufferService;

        public BaseGTINTypeService(IGTINQueries gTINQueries, IGTINRepository gTINRepository,
                            ISessionBufferService sessionBufferService)
        {
            this._gTINQueries = gTINQueries;
            this._gTINRepository = gTINRepository;

            this._sessionBufferService = sessionBufferService;
        }

        public GTIN CalculateCheckDigit(GTIN gTIN)
        {
            string code = GetCode(gTIN, false);

            var sumPos = 0;
            var pos = 3;

            for (var i = code.Length - 2; i >= 0; i--)
            {
                sumPos += pos * (int)(code[i] - '0');
                pos = (pos == 3) ? 1 : 3;
            }

            gTIN.CheckDigit = (10 - sumPos % 10) % 10;
            return gTIN;
        }

        public abstract string GetCode(GTIN gTin, bool isRecheck = false);

        public virtual GTINValidationCodes ValidationFormat(GTIN gTIN)
        {
            if (gTIN.CompanyCode <= 0)
                return GTINValidationCodes.CompanyCodeMustBeGreaterThanZero;

            if (gTIN.CompanyCode.ToString().Length > this.MaxLengthNumeric - 1)
                return GTINValidationCodes.CompanyCodeTooLong;

            if (gTIN.Numeric <= 0)
                return GTINValidationCodes.NumericMustBeGreaterThanZero;

            if (gTIN.Numeric.ToString().Length > this.MaxLengthNumeric - 1)
                return GTINValidationCodes.NumericTooLong;

            var gTINChecking = CalculateCheckDigit(gTIN);
            if (gTINChecking == null || gTINChecking.CheckDigit != gTIN.CheckDigit)
                return GTINValidationCodes.CheckDigitInValid;
            return GTINValidationCodes.GTINValid;

        }

        public virtual async Task<GTINValidationCodes> CheckNewGTINAsync(Company company, GTIN gTIN, UserSession session)
        {
            var rs = this.ValidationFormat(gTIN);
            if (rs != GTINValidationCodes.GTINValid)
                throw rs.GetException();

            var gTINs = await _gTINQueries.GetByCompanyCodeAsync(company.GS1Code);
            if (gTINs.Where(x => x.Numeric == gTIN.Numeric && x.PartnerId == company.Id).Count() > 0)
                return GTINValidationCodes.AlreadyExist;

            var sessionBuffers = await _sessionBufferService.GetByCompanyIdAsync(company.Id, SessionBufferTypes.GTIN);
            var now = DateTime.Now;
            if (sessionBuffers.Where(x => x.ExpiredDate > now && x.SessionId != session.SessionId && (JsonConvert.DeserializeObject<GTIN>(x.DataJson)?.Numeric ?? 0) == gTIN.Numeric)
                .Count() > 0)
                return GTINValidationCodes.UsedByAnotherSession;

            return GTINValidationCodes.GTINValid;
        }
    }
}