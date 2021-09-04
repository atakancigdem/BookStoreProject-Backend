using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IBookImageService
    {
        IResult Add(IFormFile file, BookImage bookImage);
        IResult Update(IFormFile file, BookImage bookImage);
        IResult Delete(BookImage bookImage);
        IDataResult<List<BookImage>> GetAll();
        IDataResult<BookImage> GetById(int bookImageId);
        IDataResult<List<BookImage>> GetImagesByBookId(int bookId);
    }
}
