using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSubheadingOfSubheadingDal : EfEntityRepositoryBase<SubheadingOfSubheading, BookContext>, ISubheadingOfSubheadingDal
    {
        public List<SubheadingOfSubheadingDetailDto> GetSubheadingOfSubheadingDetails()
        {
            using (BookContext context = new BookContext())
            {
                var result = from sos in context.SubheadingsOfSubheading
                    join c in context.Categories on sos.CategoryId equals c.CategoryId
                    join s in context.Subheadings on sos.SubheadingId equals s.SubheadingId

                    select new SubheadingOfSubheadingDetailDto
                    {
                        SubheadingOfSubheadingId = sos.SubheadingOfSubheadingId,
                        CategoryName = c.CategoryName,
                        SubheadingName = s.SubheadingName,
                        SubheadingOfSubheadingName = sos.SubheadingOfSubheadingName
                    };

                return result.ToList();
            }
        }
    }
}
