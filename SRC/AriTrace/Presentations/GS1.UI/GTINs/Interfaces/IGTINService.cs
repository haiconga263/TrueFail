using Common.Interfaces;
using Common.Models;
using GS1.UI.GTINs.Enumerations;
using GS1.UI.GTINs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.GTINs.Interfaces
{
    public interface IGTINService : IBaseService
    {
        Task<GTIN> GenerateGTINAsync(int partnerId, GTINTypes gTINTypes, UserSession session);
        Task<GTINValidationCodes> CheckNewGTINAsync(int partnerId, GTIN gTIN, UserSession session);
        Task<string> GetCodeGTINAsync(GTIN gTIN);
        Task<GTIN> CalculateCheckDigitAsync(GTIN gTIN);
        Task<GTIN> InsertOrUpdateGTINAsync(int partnerId, GTIN gTIN, UserSession session);
    }
}
