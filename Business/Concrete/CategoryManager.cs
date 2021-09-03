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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("Category.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Category>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList(), Messages.CategoriesListed);
        }

        public IDataResult<Category> GetByCategory(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId), Messages.CategoryFoundById);
        }

        public IDataResult<Category> GetByCategoryName(string categoryName)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryName == categoryName), Messages.CategoryFoundByName);
        }

        [SecuredOperation("Category.Add, Admin")]
        [ValidationAspect(typeof(CategoryValidator), Priority = 1)]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryNameExists(category.CategoryName));

            if (result != null)
            {
                return result;
            }

            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        [SecuredOperation("Category.Update, Admin")]
        [ValidationAspect(typeof(CategoryValidator), Priority = 1)]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Update(Category category)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryNameExists(category.CategoryName));

            if (result != null)
            {
                return result;
            }

            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdate);
        }

        [SecuredOperation("Category.Delete, Admin")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDelete);
        }

        private IResult CheckIfCategoryNameExists(string categoryName)
        {

            var result = _categoryDal.GetList(c => c.CategoryName == categoryName).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Category category)
        {
            _categoryDal.Update(category);
            _categoryDal.Add(category);
            return new SuccessResult();
        }
    }
}
