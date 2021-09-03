using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.DTOs
{
    public class SubheadingOfSubheadingDetailDto : IDto
    {
        public int SubheadingOfSubheadingId { get; set; }
        public string CategoryName { get; set; }
        public string SubheadingName { get; set; }
        public string SubheadingOfSubheadingName { get; set; }
    }
}
