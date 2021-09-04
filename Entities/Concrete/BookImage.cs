using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class BookImage : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ImagePath { get; set; }
        public DateTime? Date { get; set; }
    }
}
