using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        //IDataResult<List<User>> GetAll();
        //IDataResult<List<OperationClaim>> GetClaims(User user);
        //IDataResult<List<User>> GetListByFirstName(string firstName);
        //IDataResult<List<User>> GetListByLastName(string lastName);
        //IDataResult<List<User>> GetListByName(string firstName, string lastName);
        //IResult Add(User user);
        //IResult Update(User user);
        //IResult Delete(User user);
        //IDataResult<User> GetByMail(string email);

        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
    }
}