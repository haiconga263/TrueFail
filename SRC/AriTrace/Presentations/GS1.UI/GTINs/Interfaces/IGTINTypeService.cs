using Common.Models;
using GS1.UI.GTINs.Enumerations;
using GS1.UI.GTINs.Models;
using MDM.UI.Companies.Models;
using System.Threading.Tasks;

namespace GS1.UI.Interfaces
{
    public interface IGTINTypeService
    {
        int MaxLengthNumeric { get; }

        string GetCode(GTIN gTin, bool isRecheck = false);

        GTINValidationCodes ValidationFormat(GTIN gTIN);

        GTIN CalculateCheckDigit(GTIN gTIN);

        Task<GTINValidationCodes> CheckNewGTINAsync(Company company, GTIN gTIN, UserSession session);
    }
}
