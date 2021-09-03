using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ILanguageService
    {
        IDataResult<List<Language>> GetList();
        IDataResult<Language> GetById(int languageId);
        IDataResult<Language> GetByName(string languageName);
        IResult Add(Language language);
        IResult Update(Language language);
        IResult Delete(Language language);
    }
}
