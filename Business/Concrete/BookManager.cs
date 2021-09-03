using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly ICategoryService _categoryService;

        public BookManager(IBookDal bookDal, ICategoryService categoryService)
        {
            _bookDal = bookDal;
            _categoryService = categoryService;
        }

        public IDataResult<List<Book>> GetByBookName(string bookName)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.BookName == bookName).ToList(), Messages.BooksListedByName);
        }

        public IDataResult<Book> GetById(int bookId)
        {
            return new SuccessDataResult<Book>(_bookDal.Get(b => b.Id == bookId), Messages.BookFoundById);
        }

        public IDataResult<List<BookDetailDto>> GetBookDetails()
        {
            return new SuccessDataResult<List<BookDetailDto>>(_bookDal.GetBookDetails().ToList(), Messages.BookDetails);
        }

        [SecuredOperation("Book.List,Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Book>> GetAll()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Book>>(_bookDal.GetList().ToList(), Messages.BooksListed);
        }

        public IDataResult<List<Book>> GetListByAuthorId(int authorId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.AuthorId == authorId).ToList(), Messages.BookListedByAuthor);
        }

        [SecuredOperation("Book.List,Admin")]
        [LogAspect(typeof(FileLogger))]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Book>> GetListByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.CategoryId == categoryId).ToList(), Messages.BookListedByCategory);
        }

        public IDataResult<List<Book>> GetListByLanguageId(int languageId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.LanguageId == languageId).ToList(), Messages.BookListedByLanguage);
        }

        public IDataResult<List<Book>> GetListByUnitPrice(decimal minPrice, decimal maxPrice)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.Price >= minPrice && b.Price <= maxPrice).ToList(), Messages.BookListedByPrice);
        }

        public IDataResult<List<Book>> GetListByPublisherId(int publisherId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.PublisherId == publisherId).ToList(), Messages.BookListedByPublisher);
        }

        public IDataResult<List<Book>> GetListBySubheadingId(int subheadingId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.SubheadingId == subheadingId).ToList(), Messages.BookListedBySubheading);
        }

        public IDataResult<List<Book>> GetListBySubheadingOfSubheadingId(int sosId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.SubheadingOfSubheadingId == sosId).ToList(), Messages.BookListedBySOS);
        }

        [SecuredOperation("Book.Add,Admin")]
        [ValidationAspect(typeof(BookValidator), Priority = 1)]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Add(Book book)
        {
            IResult result = BusinessRules.Run(CheckIfBookNameExists(book.BookName), CheckIfCategoryIsEnabled());

            if (result != null)
            {
                return result;
            }

            _bookDal.Add(book);
            return new SuccessResult(Messages.BookAdded);
        }

        [SecuredOperation("Book.Update,Admin")]
        [ValidationAspect(typeof(BookValidator), Priority = 1)]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Update(Book book)
        {
            IResult result = BusinessRules.Run(CheckIfBookNameExists(book.BookName), CheckIfCategoryIsEnabled());

            if (result != null)
            {
                return result;
            }

            _bookDal.Update(book);
            return new SuccessResult(Messages.BookUpdated);
        }

        [SecuredOperation("Book.Delete,Admin")]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Delete(Book book)
        {
            _bookDal.Delete(book);
            return new SuccessResult(Messages.BookDelete);
        }

        private IResult CheckIfBookNameExists(string bookName)
        {

            var result = _bookDal.GetList(p => p.BookName == bookName).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryIsEnabled()
        {
            var result = _categoryService.GetList();
            if (result.Data.Count < 10)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Book book)
        {
            _bookDal.Update(book);
            _bookDal.Add(book);
            return new SuccessResult();
        }
    }
}
