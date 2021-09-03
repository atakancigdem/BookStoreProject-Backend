using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        //[SecuredOperation("User.List, Admin")]
        //[PerformanceAspect(5)]
        //[CacheAspect(duration: 10)]
        //public IDataResult<List<User>> GetAll()
        //{
        //    Thread.Sleep(5000);
        //    return new SuccessDataResult<List<User>>(_userDal.GetList().ToList());
        //}

        //public IDataResult<List<OperationClaim>> GetClaims(User user)
        //{
        //    return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user).ToList());
        //}

        //public IDataResult<List<User>> GetListByFirstName(string firstName)
        //{
        //    return new SuccessDataResult<List<User>>(_userDal.GetList(u => u.FirstName == firstName).ToList());
        //}

        //public IDataResult<List<User>> GetListByLastName(string lastName)
        //{
        //    return new SuccessDataResult<List<User>>(_userDal.GetList(u => u.LastName == lastName).ToList());
        //}

        //public IDataResult<List<User>> GetListByName(string firstName, string lastName)
        //{
        //    return new SuccessDataResult<List<User>>(_userDal.GetList(u =>
        //        u.FirstName == firstName && u.LastName == lastName).ToList());
        //}

        //[SecuredOperation("User.Add, Admin")]
        //[ValidationAspect(typeof(UserValidator))]
        //[CacheRemoveAspect("IUserService.Get")]
        //public IResult Add(User user)
        //{
        //    IResult result = BusinessRules.Run(CheckIfEmailExists(user.Email));

        //    if (result != null)
        //    {
        //        return result;
        //    }

        //    _userDal.Add(user);
        //    return new SuccessResult();
        //}

        //[SecuredOperation("User.Update, Admin")]
        //[ValidationAspect(typeof(UserValidator))]
        //[CacheRemoveAspect("IUserService.Get")]
        //public IResult Update(User user)
        //{
        //    IResult result = BusinessRules.Run(CheckIfEmailExists(user.Email));

        //    if (result != null)
        //    {
        //        return result;
        //    }

        //    _userDal.Update(user);
        //    return new SuccessResult();
        //}

        //[SecuredOperation("User.Delete, Admin")]
        //[CacheRemoveAspect("IUserService.Get")]
        //public IResult Delete(User user)
        //{
        //    _userDal.Delete(user);
        //    return new SuccessResult();
        //}

        //public IDataResult<User> GetByMail(string email)
        //{
        //    return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        //}

        //private IResult CheckIfEmailExists(string email)
        //{

        //    var result = _userDal.GetList(u => u.Email == email).Any();
        //    if (result)
        //    {
        //        return new ErrorResult();
        //    }

        //    return new SuccessResult();
        //}

        //[TransactionScopeAspect]
        //public IResult TransactionalOperation(User user)
        //{
        //    _userDal.Update(user);
        //    _userDal.Add(user);
        //    return new SuccessResult();
        //}

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}