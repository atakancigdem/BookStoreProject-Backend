using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SubheadingOfSubheadingValidator : AbstractValidator<SubheadingOfSubheading>
    {
        public SubheadingOfSubheadingValidator()
        {
            RuleFor(sos => sos.CategoryId).NotEmpty();
            RuleFor(sos => sos.SubheadingId).NotEmpty();
            RuleFor(sos => sos.SubheadingOfSubheadingName).NotEmpty();
        }
    }
}
