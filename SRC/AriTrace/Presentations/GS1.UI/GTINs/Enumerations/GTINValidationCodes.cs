namespace GS1.UI.GTINs.Enumerations
{
    public enum GTINValidationCodes
    {
        GTINValid = 0,
        CompanyCodeMustBeGreaterThanZero = 1,
        CompanyCodeTooLong = 2,
        NumericMustBeGreaterThanZero = 3,
        NumericTooLong = 4,
        CheckDigitInValid = 5,
        AlreadyExist = 6,
        UsedByAnotherSession = 7,
        BaseNotExist = 8
    }
}
