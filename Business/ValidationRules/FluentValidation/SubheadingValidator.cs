﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SubheadingValidator : AbstractValidator<Subheading>
    {
        public SubheadingValidator()
        {
            RuleFor(s => s.CategoryId).NotEmpty();
            RuleFor(s => s.SubheadingName).NotEmpty();
        }
    }
}
