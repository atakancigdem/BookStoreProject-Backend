using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class BookImageManager : IBookImageService
    {

        private readonly IBookImageDal _bookImageDal;

        public BookImageManager(IBookImageDal bookImageDal)
        {
            _bookImageDal = bookImageDal;
        }

        [SecuredOperation("BookImage.Add, admin")]
        [ValidationAspect(typeof(BookImageValidator), Priority = 1)]
        [CacheRemoveAspect("IBookImageService.Get")]
        public IResult Add(IFormFile file, BookImage bookImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(bookImage.BookId));
            if (result != null)
            {
                return result;
            }
            bookImage.ImagePath = FileHelper.Add(file);
            bookImage.Date = DateTime.Now;

            _bookImageDal.Add(bookImage);

            return new SuccessResult(Messages.BookImageAdded);
        }

        [SecuredOperation("BookImage.Delete, admin")]
        [CacheRemoveAspect("IBookImageService.Get")]
        public IResult Delete(BookImage bookImage)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _bookImageDal.Get(b => b.Id == bookImage.Id).ImagePath;
            IResult result = BusinessRules.Run(FileHelper.Delete(oldPath));

            if (result != null)
            {
                return result;
            }

            _bookImageDal.Delete(bookImage);

            return new SuccessResult(Messages.BookImageDeleted);
        }

        [SecuredOperation("BookImage.List, admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<BookImage>> GetAll()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<BookImage>>(_bookImageDal.GetList(), Messages.BookImagesListed);
        }

        public IDataResult<BookImage> GetById(int id)
        {
            return new SuccessDataResult<BookImage>(_bookImageDal.Get(b => b.BookId == id));
        }

        [SecuredOperation("BookImage.Update, admin")]
        [ValidationAspect(typeof(BookImageValidator), Priority = 1)]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Update(IFormFile file, BookImage bookImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(bookImage.BookId));
            if (result != null)
            {
                return result;
            }

            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _bookImageDal.Get(b => b.Id == bookImage.Id).ImagePath;

            bookImage.ImagePath = FileHelper.Update(oldPath, file);
            bookImage.Date = DateTime.Now;
            _bookImageDal.Update(bookImage);

            return new SuccessResult(Messages.BookImageUpdated);
        }

        public IDataResult<List<BookImage>> GetImagesByBookId(int bookId)
        {
            IResult result = BusinessRules.Run(CheckIfBookImageNull(bookId));
            if (result != null)
            {
                return new ErrorDataResult<List<BookImage>>(result.Message);
            }

            return new SuccessDataResult<List<BookImage>>(CheckIfBookImageNull(bookId).Data);
        }

        private IResult CheckImageLimitExceeded(int bookId)
        {
            var bookImageCount = _bookImageDal.GetList(b => b.BookId == bookId).Count;
            if (bookImageCount >= 5)
            {
                return new ErrorResult(Messages.BookImageLimitExceeded);
            }

            return new SuccessResult();
        }
        private IDataResult<List<BookImage>> CheckIfBookImageNull(int id)
        {
            try
            {
                string path = @"\images\DefaultImage.jpg";
                var result = _bookImageDal.GetList(b => b.BookId == id).Any();
                if (!result)
                {
                    List<BookImage> bookImage = new List<BookImage>();
                    bookImage.Add(new BookImage { BookId = id, ImagePath = path, Date = DateTime.Now });

                    return new SuccessDataResult<List<BookImage>>(bookImage);
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<BookImage>>(exception.Message);
            }

            return new SuccessDataResult<List<BookImage>>(_bookImageDal.GetList(b=> b.BookId == id));
        }
    }
}
