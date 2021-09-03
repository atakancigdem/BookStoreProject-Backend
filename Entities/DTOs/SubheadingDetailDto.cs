using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.DTOs
{
    public class SubheadingDetailDto : IDto
    {
        public int SubheadingId { get; set; }
        public string CategoryName { get; set; }
        public string SubheadingName { get; set; }
    }
}
