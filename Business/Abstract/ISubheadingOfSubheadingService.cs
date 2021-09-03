using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ISubheadingOfSubheadingService
    {
        IDataResult<List<SubheadingOfSubheading>> GetList();
        IDataResult<List<SubheadingOfSubheadingDetailDto>> GetSubheadingOfSubheadingDetails();
        IDataResult<List<SubheadingOfSubheading>> GetListByCategoryId(int categoryId);
        IDataResult<List<SubheadingOfSubheading>> GetListBySubheadingId(int subheadingId);
        IDataResult<SubheadingOfSubheading> GetById(int sosId);
        IDataResult<SubheadingOfSubheading> GetByName(string sosName);
        IResult Add(SubheadingOfSubheading subheadingOfSubheading);
        IResult Update(SubheadingOfSubheading subheadingOfSubheading);
        IResult Delete(SubheadingOfSubheading subheadingOfSubheading);
    }
}
