using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookDal : EfEntityRepositoryBase<Book, BookContext>, IBookDal
    {
        public List<BookDetailDto> GetBookDetails()
        {
            using (BookContext context = new BookContext())
            {
                var result = from book in context.Books
                    join category in context.Categories on book.CategoryId equals category.CategoryId
                    join subheading in context.Subheadings on book.SubheadingId equals subheading.SubheadingId
                    join sos in context.SubheadingsOfSubheading on book.SubheadingOfSubheadingId equals sos.SubheadingOfSubheadingId
                    join author in context.Authors on book.AuthorId equals author.AuthorId
                    join language in context.Languages on book.LanguageId equals language.LanguageId
                    join publisher in context.Publishers on book.PublisherId equals publisher.PublisherId
                    
                    select new BookDetailDto
                    {
                        Id = book.Id,
                        CategoryName = category.CategoryName,
                        SubheadingName = subheading.SubheadingName,
                        SubheadingOfSubheadingName = sos.SubheadingOfSubheadingName,
                        AuthorName = author.AuthorName,
                        LanguageName = language.LanguageName,
                        PublisherName = publisher.PublisherName,
                        BookName = book.BookName,
                        Price = book.Price,
                        StockQty = book.StockQty,
                        Explanation = book.Explanation
                    };

                return result.ToList();
            } 
        }
    }
}