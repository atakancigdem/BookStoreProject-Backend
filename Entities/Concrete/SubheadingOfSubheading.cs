using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class SubheadingOfSubheading : IEntity
    {
        public int SubheadingOfSubheadingId { get; set; }
        public int CategoryId { get; set; }
        public int SubheadingId { get; set; }
        public string SubheadingOfSubheadingName { get; set; }
    }
}
