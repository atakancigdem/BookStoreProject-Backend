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
    public class EfSubheadingDal : EfEntityRepositoryBase<Subheading, BookContext>, ISubheadingDal
    {
        public List<SubheadingDetailDto> GetSubheadingDetails()
        {
            using (BookContext context = new BookContext())
            {
                var result = from sub in context.Subheadings
                    join c in context.Categories on sub.CategoryId equals c.CategoryId

                    select new SubheadingDetailDto
                    {
                        SubheadingId = sub.SubheadingId,
                        CategoryName = c.CategoryName,
                        SubheadingName = sub.SubheadingName
                    };

                return result.ToList();
            }
        }
    }
}
