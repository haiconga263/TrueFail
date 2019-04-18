using Common.Exceptions;
using GS1.GTINs.Services;
using GS1.UI.GTINs.Enumerations;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Interfaces;
using GS1.UI.SessionBuffers.Interfaces;
using System;

namespace GS1.GTINs.Factories
{
    public static class GTINFactory
    {
        public static IGTINTypeService GetGTINService(this GTINTypes gTINTypes,
            IGTINQueries gTINQueries, IGTINRepository gTINRepository,
            ISessionBufferService _sessionBufferService)
        {
            switch (gTINTypes)
            {
                case GTINTypes.gtin_8:
                    return new GTIN8Service(gTINQueries, gTINRepository, _sessionBufferService);
                case GTINTypes.gtin_12:
                    return new GTIN12Service(gTINQueries, gTINRepository, _sessionBufferService);
                case GTINTypes.gtin_13:
                    return new GTIN13Service(gTINQueries, gTINRepository, _sessionBufferService);
                case GTINTypes.gtin_14:
                    return new GTIN14Service(gTINQueries, gTINRepository, _sessionBufferService);
                default:
                    return new GTIN13Service(gTINQueries, gTINRepository, _sessionBufferService);
            }
        }

        public static Exception GetException(this GTINValidationCodes gTINValidationCodes)
        {
            switch (gTINValidationCodes)
            {
                case GTINValidationCodes.CompanyCodeMustBeGreaterThanZero:
                    return new BusinessException("GTIN.CompanyCodeMustBeGreaterThanZero");
                case GTINValidationCodes.CompanyCodeTooLong:
                    return new BusinessException("GTIN.CompanyCodeTooLong");
                case GTINValidationCodes.NumericMustBeGreaterThanZero:
                    return new BusinessException("GTIN.NumericMustBeGreaterThanZero");
                case GTINValidationCodes.NumericTooLong:
                    return new BusinessException("GTIN.NumericTooLong");
                case GTINValidationCodes.CheckDigitInValid:
                    return new BusinessException("GTIN.CheckDigitInValid");
                case GTINValidationCodes.AlreadyExist:
                    return new BusinessException("GTIN.AlreadyExist");
                case GTINValidationCodes.UsedByAnotherSession:
                    return new BusinessWarningException("GTIN.UsedByAnotherSession");
                case GTINValidationCodes.BaseNotExist:
                    return new BusinessException("GTIN.BaseNotExist");
                case GTINValidationCodes.GTINValid:
                default:
                    return null;
            }
        }
    }
}
