using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Commands.Validations
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(reg => reg.UserName).NotEmpty().WithMessage("Validation.NotEmptyUserName");
            RuleFor(reg => reg.Password).NotEmpty().WithMessage("Validation.NotEmptyPassword");
        }
    }
}
