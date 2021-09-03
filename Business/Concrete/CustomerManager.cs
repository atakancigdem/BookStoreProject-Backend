using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [SecuredOperation("Customer.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Customer>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Customer>>(_customerDal.GetList().ToList());
        }

        public IDataResult<List<Customer>> GetListByUserId(int userId)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetList(c => c.UserId == userId).ToList());
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == customerId));
        }

        public IDataResult<List<Customer>> GetListByCompanyName(string companyName)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetList(c => c.CompanyName == companyName).ToList());
        }

        [SecuredOperation("Customer.Add, Admin")]
        [ValidationAspect(typeof(CustomerValidator), Priority = 1)]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult();
        }

        [SecuredOperation("Customer.Update, Admin")]
        [ValidationAspect(typeof(CustomerValidator), Priority = 1)]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult();
        }

        [SecuredOperation("Customer.Delete, Admin")]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult();
        }
    }
}
