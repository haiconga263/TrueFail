using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Commands.Validations
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(reg => reg.UserName).NotEmpty().WithMessage("Validation.NotEmptyUserName");
            RuleFor(reg => reg.Password).NotEmpty().WithMessage("Validation.NotEmptyPassword");
            RuleFor(reg => reg.PinCode).NotEmpty().WithMessage("Validation.NotEmptyPinCode");
        }
    }
}
