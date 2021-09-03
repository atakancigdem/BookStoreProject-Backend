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
    public class PublisherManager : IPublisherService
    {
        private readonly IPublisherDal _publisherDal;

        public PublisherManager(IPublisherDal publisherDal)
        {
            _publisherDal = publisherDal;
        }

        [SecuredOperation("Publisher.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Publisher>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Publisher>>(_publisherDal.GetList().ToList(), Messages.PublishersListed);
        }

        public IDataResult<Publisher> GetById(int publisherId)
        {
            return new SuccessDataResult<Publisher>(_publisherDal.Get(l => l.PublisherId == publisherId), Messages.PublisherFoundById);
        }

        public IDataResult<Publisher> GetByName(string publisherName)
        {
            return new SuccessDataResult<Publisher>(_publisherDal.Get(p => p.PublisherName == publisherName), Messages.PublisherFoundByName);
        }

        [SecuredOperation("Publisher.Add, Admin")]
        [ValidationAspect(typeof(PublisherValidator), Priority = 1)]
        [CacheRemoveAspect("IPublisherService.Get")]
        public IResult Add(Publisher publisher)
        {
            IResult result = BusinessRules.Run(CheckIfPublisherNameExists(publisher.PublisherName));

            if (result != null)
            {
                return result;
            }

            _publisherDal.Add(publisher);
            return new SuccessResult(Messages.PublisherAdded);
        }

        [SecuredOperation("Publisher.Update, Admin")]
        [ValidationAspect(typeof(PublisherValidator), Priority = 1)]
        [CacheRemoveAspect("IPublisherService.Get")]
        public IResult Update(Publisher publisher)
        {
            IResult result = BusinessRules.Run(CheckIfPublisherNameExists(publisher.PublisherName));

            if (result != null)
            {
                return result;
            }

            _publisherDal.Update(publisher);
            return new SuccessResult(Messages.PublisherUpdate);
        }

        [SecuredOperation("Publisher.Delete, Admin")]
        [CacheRemoveAspect("IPublisherService.Get")]
        public IResult Delete(Publisher publisher)
        {
            _publisherDal.Delete(publisher);
            return new SuccessResult(Messages.PublisherDelete);
        }

        private IResult CheckIfPublisherNameExists(string publisherName)
        {

            var result = _publisherDal.GetList(p => p.PublisherName == publisherName).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Publisher publisher)
        {
            _publisherDal.Update(publisher);
            _publisherDal.Add(publisher);
            return new SuccessResult();
        }

    }
}
