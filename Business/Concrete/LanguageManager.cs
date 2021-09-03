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

namespace Business.Concrete
{
    public class LanguageManager : ILanguageService
    {
        private readonly ILanguageDal _languageDal;

        public LanguageManager(ILanguageDal languageDal)
        {
            _languageDal = languageDal;
        }

        [SecuredOperation("Language.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Language>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Language>>(_languageDal.GetList().ToList(), Messages.LanguagesListed);
        }

        public IDataResult<Language> GetById(int languageId)
        {
            return new SuccessDataResult<Language>(_languageDal.Get(l => l.LanguageId == languageId), Messages.LanguageFoundById);
        }

        public IDataResult<Language> GetByName(string languageName)
        {
            return new SuccessDataResult<Language>(_languageDal.Get(l => l.LanguageName == languageName), Messages.LanguageFoundByName);
        }

        [SecuredOperation("Language.Add, Admin")]
        [ValidationAspect(typeof(LanguageValidator), Priority = 1)]
        [CacheRemoveAspect("ILanguageService.Get")]
        public IResult Add(Language language)
        {
            IResult result = BusinessRules.Run(CheckIfLanguageNameExists(language.LanguageName));

            if (result != null)
            {
                return result;
            }

            _languageDal.Add(language);
            return new SuccessResult(Messages.LanguageAdded);
        }

        [SecuredOperation("Language.Update, Admin")]
        [ValidationAspect(typeof(LanguageValidator), Priority = 1)]
        [CacheRemoveAspect("ILanguageService.Get")]
        public IResult Update(Language language)
        {
            IResult result = BusinessRules.Run(CheckIfLanguageNameExists(language.LanguageName));

            if (result != null)
            {
                return result;
            }

            _languageDal.Update(language);
            return new SuccessResult(Messages.LanguageUpdate);
        }

        [SecuredOperation("Language.Delete, Admin")]
        [CacheRemoveAspect("ILanguageService.Get")]
        public IResult Delete(Language language)
        {
            _languageDal.Delete(language);
            return new SuccessResult(Messages.LanguageDelete);
        }

        private IResult CheckIfLanguageNameExists(string languageName)
        {

            var result = _languageDal.GetList(l => l.LanguageName == languageName).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
