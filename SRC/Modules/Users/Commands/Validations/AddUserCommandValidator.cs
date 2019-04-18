using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Commands.Validations
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(reg => reg.UserName).NotEmpty().WithMessage("Validation.NotEmptyUserName");
            RuleFor(reg => reg.Password).NotEmpty().WithMessage("Validation.NotEmptyPassword");
            RuleFor(reg => reg.Email).EmailAddress().WithMessage("Validation.WrongFormatEmail");
        }
    }
}
