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
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorDal _authorDal;

        public AuthorManager(IAuthorDal authorDal)
        {
            _authorDal = authorDal;
        }

        [SecuredOperation("Author.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Author>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Author>>(_authorDal.GetList().ToList(), Messages.AuthorsListed);
        }

        public IDataResult<Author> GetById(int authorId)
        {
            return new SuccessDataResult<Author>(_authorDal.Get(a => a.AuthorId == authorId), Messages.AuthorFoundById);
        }

        public IDataResult<Author> GetByName(string authorName)
        {
            return new SuccessDataResult<Author>(_authorDal.Get(a => a.AuthorName == authorName), Messages.AuthorFoundByName);
        }

        [SecuredOperation("Author.Add, Admin")]
        [ValidationAspect(typeof(AuthorValidator), Priority = 1)]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Add(Author author)
        {
            IResult result = BusinessRules.Run(CheckIfAuthorNameExists(author.AuthorName));

            if (result != null)
            {
                return result;
            }

            _authorDal.Add(author);
            return new SuccessResult(Messages.AuthorAdded);
        }

        [SecuredOperation("Author.Update, Admin")]
        [ValidationAspect(typeof(AuthorValidator), Priority = 1)]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Update(Author author)
        {
            IResult result = BusinessRules.Run(CheckIfAuthorNameExists(author.AuthorName));

            if (result != null)
            {
                return result;
            }

            _authorDal.Update(author);
            return new SuccessResult(Messages.AuthorUpdate);
        }

        [SecuredOperation("Author.Delete, Admin")]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Delete(Author author)
        {
            _authorDal.Delete(author);
            return new SuccessResult(Messages.AuthorDelete);
        }

        private IResult CheckIfAuthorNameExists(string authorName)
        {

            var result = _authorDal.GetList(a => a.AuthorName == authorName).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Author author)
        {
            _authorDal.Update(author);
            _authorDal.Add(author);
            return new SuccessResult();
        }
    }
}
