using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int? SubheadingId { get; set; }
        public int? SubheadingOfSubheadingId { get; set; }
        public int AuthorId { get; set; }
        public int LanguageId { get; set; }
        public int PublisherId { get; set; }
        public string BookName { get; set; }
        public int Price { get; set; }
        public int StockQty { get; set; }
        public string? Explanation { get; set; }
    }
}
