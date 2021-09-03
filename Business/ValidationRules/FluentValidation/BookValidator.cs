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
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.BookName).NotEmpty();
            RuleFor(b => b.BookName).MinimumLength(1);
            RuleFor(b => b.CategoryId).NotEmpty();
            RuleFor(b => b.LanguageId).NotEmpty();
            RuleFor(b => b.AuthorId).NotEmpty();
            RuleFor(b => b.PublisherId).NotEmpty();
            RuleFor(b => b.Price).NotEmpty();
            RuleFor(b => b.Price).GreaterThan(0);
            RuleFor(b => b.StockQty).NotEmpty();
        }
    }
}
