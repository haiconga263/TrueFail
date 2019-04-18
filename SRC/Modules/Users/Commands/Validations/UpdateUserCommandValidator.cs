using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Commands.Validations
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(reg => reg.User).NotNull().WithMessage("Validation.WrongFormatEmail");
            RuleFor(reg => reg.User.Id).NotEqual(0).WithMessage("Validation.NotEmptyUserId");
            RuleFor(reg => reg.User.Email).EmailAddress().WithMessage("Validation.WrongFormatEmail");
        }
    }
}
