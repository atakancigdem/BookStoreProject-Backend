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
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class SubheadingManager : ISubheadingService
    {
        private readonly ISubheadingDal _subheadingDal;

        public SubheadingManager(ISubheadingDal subheadingDal)
        {
            _subheadingDal = subheadingDal;
        }

        [SecuredOperation("Subheading.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Subheading>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Subheading>>(_subheadingDal.GetList().ToList(), Messages.SubheadingsListed);
        }

        public IDataResult<Subheading> GetBySubheadingId(int subheadingId)
        {
            return new SuccessDataResult<Subheading>(_subheadingDal.Get(s => s.SubheadingId == subheadingId), Messages.SubheadingFoundById);
        }

        public IDataResult<Subheading> GetBySubheadingName(string subheadingName)
        {
            return new SuccessDataResult<Subheading>(_subheadingDal.Get(s => s.SubheadingName == subheadingName), Messages.SubheadingFoundByName);
        }

        public IDataResult<List<Subheading>> GetListByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Subheading>>(_subheadingDal.GetList(s => s.CategoryId == categoryId).ToList(), Messages.SubheadingListedByCategory);
        }

        [SecuredOperation("Subheading.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<SubheadingDetailDto>> GetSubheadingDetails()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<SubheadingDetailDto>>(_subheadingDal.GetSubheadingDetails().ToList(), Messages.SubheadingDetail);
        }

        [SecuredOperation("Subheading.Add, Admin")]
        [ValidationAspect(typeof(SubheadingValidator), Priority = 1)]
        [CacheRemoveAspect("ISubheadingService.Get")]
        public IResult Add(Subheading subheading)
        {
            IResult result = BusinessRules.Run(CheckIfSubheadingNameExists(subheading.SubheadingName));

            if (result != null)
            {
                return result;
            }

            _subheadingDal.Add(subheading);
            return new SuccessResult(Messages.SubheadingAdded);
        }

        [SecuredOperation("Subheading.Update, Admin")]
        [ValidationAspect(typeof(SubheadingValidator), Priority = 1)]
        [CacheRemoveAspect("ISubheadingService.Get")]
        public IResult Update(Subheading subheading)
        {
            IResult result = BusinessRules.Run(CheckIfSubheadingNameExists(subheading.SubheadingName));

            if (result != null)
            {
                return result;
            }

            _subheadingDal.Update(subheading);
            return new SuccessResult(Messages.SubheadingUpdate);
        }

        [SecuredOperation("Subheading.Delete, Admin")]
        [CacheRemoveAspect("ISubheadingService.Get")]
        public IResult Delete(Subheading subheading)
        {
            _subheadingDal.Delete(subheading);
            return new SuccessResult(Messages.SubheadingDelete);
        }

        private IResult CheckIfSubheadingNameExists(string subheadingName)
        {

            var result = _subheadingDal.GetList(s => s.SubheadingName == subheadingName).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
