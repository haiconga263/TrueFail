using GS1.UI.Interfaces;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GS1.UI.SessionBuffers.Interfaces;

namespace GS1.GTINs.Services
{
    public class GTIN12Service : BaseGTINTypeService, IGTINTypeService
    {
        public GTIN12Service(IGTINQueries gTINQueries, IGTINRepository gTINRepository, ISessionBufferService sessionBufferService)
            : base(gTINQueries, gTINRepository, sessionBufferService)
        {
        }

        public override int MaxLengthNumeric => 11;

        public override string GetCode(GTIN gTin, bool isRecheck = false)
        {
            if (isRecheck)
                return GetCode(CalculateCheckDigit(gTin), false);
            else
            {
                var companyCode = gTin.CompanyCode.ToString();
                var numeric = gTin.Numeric.ToString().PadLeft(this.MaxLengthNumeric - companyCode.Length, '0');
                return $"{companyCode}{numeric}{gTin.CheckDigit}";
            }
        }
    }
}
