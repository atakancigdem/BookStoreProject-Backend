using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPublisherService
    {
        IDataResult<List<Publisher>> GetList();
        IDataResult<Publisher> GetById(int publisherId);
        IDataResult<Publisher> GetByName(string publisherName);
        IResult Add(Publisher publisher);
        IResult Update(Publisher publisher);
        IResult Delete(Publisher publisher);
    }
}
