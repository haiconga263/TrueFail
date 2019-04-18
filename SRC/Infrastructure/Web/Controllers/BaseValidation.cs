using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Controllers
{
    public class BaseValidation<Tval, Tcom> where Tval : AbstractValidator<Tcom>
    {
        public BaseValidation()
        {
            var validation = Activator.CreateInstance(typeof(Tval));
        }
    }
}
