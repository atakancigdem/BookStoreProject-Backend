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
    public class SubheadingOfSubheadingManager : ISubheadingOfSubheadingService
    {
        private readonly ISubheadingOfSubheadingDal _subheadingOfSubheadingDal;

        public SubheadingOfSubheadingManager(ISubheadingOfSubheadingDal subheadingOfSubheadingDal)
        {
            _subheadingOfSubheadingDal = subheadingOfSubheadingDal;
        }

        [SecuredOperation("SubheadingOfSubheading.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<SubheadingOfSubheading>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<SubheadingOfSubheading>>(_subheadingOfSubheadingDal.GetList().ToList(), Messages.SOSsListed);
        }

        public IDataResult<List<SubheadingOfSubheadingDetailDto>> GetSubheadingOfSubheadingDetails()
        {
            return new SuccessDataResult<List<SubheadingOfSubheadingDetailDto>>(_subheadingOfSubheadingDal
                .GetSubheadingOfSubheadingDetails().ToList(), Messages.SOSDetail);
        }

        public IDataResult<List<SubheadingOfSubheading>> GetListByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<SubheadingOfSubheading>>(
                _subheadingOfSubheadingDal.GetList(sos => sos.CategoryId == categoryId).ToList(), Messages.SOSFoundByCategoryId);
        }

        public IDataResult<List<SubheadingOfSubheading>> GetListBySubheadingId(int subheadingId)
        {
            return new SuccessDataResult<List<SubheadingOfSubheading>>(
                _subheadingOfSubheadingDal.GetList(sos => sos.SubheadingId == subheadingId).ToList(), Messages.SOSFoundBySubheadingId);
        }

        public IDataResult<SubheadingOfSubheading> GetById(int sosId)
        {
            return new SuccessDataResult<SubheadingOfSubheading>(
                _subheadingOfSubheadingDal.Get(sos => sos.SubheadingOfSubheadingId == sosId), Messages.SOSFoundById);
        }

        public IDataResult<SubheadingOfSubheading> GetByName(string sosName)
        {
            return new SuccessDataResult<SubheadingOfSubheading>(
                _subheadingOfSubheadingDal.Get(sos => sos.SubheadingOfSubheadingName == sosName), Messages.SOSFoundByName);
        }

        [SecuredOperation("SubheadingOfSubheading.Add, Admin")]
        [ValidationAspect(typeof(SubheadingOfSubheadingValidator), Priority = 1)]
        [CacheRemoveAspect("ISubheadingOfSubheadingService.Get")]
        public IResult Add(SubheadingOfSubheading subheadingOfSubheading)
        {
            IResult result = BusinessRules.Run(CheckIfSubheadingOfSubheadingNameExists(subheadingOfSubheading.SubheadingOfSubheadingName));

            if (result != null)
            {
                return result;
            }

            _subheadingOfSubheadingDal.Add(subheadingOfSubheading);
            return new SuccessResult(Messages.SOSAdded);
        }

        [SecuredOperation("SubheadingOfSubheading.Update, Admin")]
        [ValidationAspect(typeof(SubheadingOfSubheadingValidator), Priority = 1)]
        [CacheRemoveAspect("ISubheadingOfSubheadingService.Get")]
        public IResult Update(SubheadingOfSubheading subheadingOfSubheading)
        {
            IResult result = BusinessRules.Run(CheckIfSubheadingOfSubheadingNameExists(subheadingOfSubheading.SubheadingOfSubheadingName));

            if (result != null)
            {
                return result;
            }

            _subheadingOfSubheadingDal.Update(subheadingOfSubheading);
            return new SuccessResult(Messages.SOSUpdate);
        }

        [SecuredOperation("SubheadingOfSubheading.Delete, Admin")]
        [ValidationAspect(typeof(SubheadingOfSubheadingValidator), Priority = 1)]
        [CacheRemoveAspect("ISubheadingOfSubheadingService.Get")]
        public IResult Delete(SubheadingOfSubheading subheadingOfSubheading)
        {
            _subheadingOfSubheadingDal.Delete(subheadingOfSubheading);
            return new SuccessResult(Messages.SOSDelete);
        }

        private IResult CheckIfSubheadingOfSubheadingNameExists(string subheadingOfSubheadingName)
        {

            var result = _subheadingOfSubheadingDal.GetList(s => s.SubheadingOfSubheadingName == subheadingOfSubheadingName).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
