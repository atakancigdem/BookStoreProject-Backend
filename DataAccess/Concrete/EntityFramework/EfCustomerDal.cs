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
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, BookContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (BookContext context = new BookContext())
            {
                var result = from customer in context.Customers
                    join user in context.Users on customer.UserId equals user.Id
                    select new CustomerDetailDto
                    {
                        Id = customer.Id,
                        Name = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        CompanyName = customer.CompanyName,
                        Status = user.Status
                    };
                return result.ToList();
            }
        }
    }
}
