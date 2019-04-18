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

namespace GS1.GTINs.Services
{
    public class GTIN14Service : BaseGTINTypeService, IGTINTypeService
    {
        public GTIN14Service(IGTINQueries gTINQueries, IGTINRepository gTINRepository, ISessionBufferService sessionBufferService)
            : base(gTINQueries, gTINRepository, sessionBufferService)
        {
        }

        public override int MaxLengthNumeric => 12;

        public override string GetCode(GTIN gTin, bool isRecheck = false)
        {
            if (isRecheck)
                return GetCode(CalculateCheckDigit(gTin), false);
            else
            {
                var companyCode = gTin.CompanyCode.ToString();
                var numeric = gTin.Numeric.ToString().PadLeft(this.MaxLengthNumeric - companyCode.Length, '0');
                return $"{gTin.IndicatorDigit}{companyCode}{numeric}{gTin.CheckDigit}";
            }
        }

        public override async Task<GTINValidationCodes> CheckNewGTINAsync(Company company, GTIN gTIN, UserSession session)
        {
            var rs = this.ValidationFormat(gTIN);
            if (rs != GTINValidationCodes.GTINValid)
                throw rs.GetException();

            var gTINs = await _gTINQueries.GetByCompanyCodeAsync(company.GS1Code);
            if (gTINs.Where(x => x.Numeric == gTIN.Numeric && x.IndicatorDigit == gTIN.IndicatorDigit && x.PartnerId == company.Id).Count() > 0)
                return GTINValidationCodes.AlreadyExist;

            if (gTIN.IndicatorDigit > 0
                && gTINs.Where(x => x.Numeric == gTIN.Numeric && x.IndicatorDigit == 0 && x.PartnerId == company.Id).Count() <= 0)
                return GTINValidationCodes.BaseNotExist;

            var sessionBuffers = await _sessionBufferService.GetByCompanyIdAsync(company.Id, SessionBufferTypes.GTIN);
            var now = DateTime.Now;
            if (sessionBuffers
                .Select(x => new { Buffer = x, Data = JsonConvert.DeserializeObject<GTIN>(x.DataJson) })
                .Where(x => x.Buffer.ExpiredDate > now
                            && x.Buffer.SessionId != session.SessionId
                            && x.Buffer.PartnerId == company.Id
                            && (x.Data?.Numeric ?? 0) == gTIN.Numeric
                            && (x.Data?.IndicatorDigit ?? 0) == gTIN.IndicatorDigit)
                .Count() > 0)
                return GTINValidationCodes.UsedByAnotherSession;

            return GTINValidationCodes.GTINValid;
        }
    }
}
