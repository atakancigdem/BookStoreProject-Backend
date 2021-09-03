using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetList();
        IDataResult<List<Customer>> GetListByUserId(int userId);
        IDataResult<Customer> GetById(int customerId);
        IDataResult<List<Customer>> GetListByCompanyName(string companyName);
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
    }
}
