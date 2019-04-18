using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Commands.Validations
{
    public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailCommandValidator()
        {
            RuleFor(reg => reg.Email).EmailAddress().WithMessage("Validation.WrongFormatEmail");
        }
    }
}
