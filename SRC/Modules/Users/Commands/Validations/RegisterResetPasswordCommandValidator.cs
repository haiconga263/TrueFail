using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Commands.Validations
{
    public class RegisterResetPasswordCommandValidator : AbstractValidator<RegisterResetPasswordCommand>
    {
        public RegisterResetPasswordCommandValidator()
        {
            RuleFor(reg => reg.UserName).NotEmpty().WithMessage("Validation.NotEmptyUserName");
        }
    }
}
