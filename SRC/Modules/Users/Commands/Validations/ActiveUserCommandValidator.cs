using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Commands.Validations
{
    public class ActiveUserCommandValidator : AbstractValidator<ActiveUserPasswordCommand>
    {
        public ActiveUserCommandValidator()
        {
            RuleFor(reg => reg.UserName).NotEmpty().WithMessage("Validation.NotEmptyUserName");
            RuleFor(reg => reg.Password).NotEmpty().WithMessage("Validation.NotEmptyPassword");
        }
    }
}
