using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using log4net.DateFormatter;

namespace Business.Abstract
{
    public interface ISubheadingService
    {
        IDataResult<List<Subheading>> GetList();
        IDataResult<Subheading> GetBySubheadingId(int subheadingId);
        IDataResult<Subheading> GetBySubheadingName(string subheadingName);
        IDataResult<List<Subheading>> GetListByCategoryId(int categoryId);
        IDataResult<List<SubheadingDetailDto>> GetSubheadingDetails();
        IResult Add(Subheading subheading);
        IResult Update(Subheading subheading);
        IResult Delete(Subheading subheading);
    }
}
