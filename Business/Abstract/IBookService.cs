using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IBookService
    {
        IDataResult<List<Book>> GetAll();
        IDataResult<List<Book>> GetByBookName(string bookName);
        IDataResult<Book> GetById(int bookId);
        IDataResult<List<BookDetailDto>> GetBookDetails();
        IDataResult<List<Book>> GetListByCategoryId(int categoryId);
        IDataResult<List<Book>> GetListBySubheadingId(int subheadingId);
        IDataResult<List<Book>> GetListBySubheadingOfSubheadingId(int sosId);
        IDataResult<List<Book>> GetListByAuthorId(int authorId);
        IDataResult<List<Book>> GetListByPublisherId(int publisherId);
        IDataResult<List<Book>> GetListByLanguageId(int languageId);
        IDataResult<List<Book>> GetListByUnitPrice(decimal minPrice, decimal maxPrice);
        IResult Add(Book book);
        IResult Update(Book book);
        IResult Delete(Book book);
    }
}
