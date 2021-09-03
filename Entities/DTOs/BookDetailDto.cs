using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.DTOs
{
    public class BookDetailDto : IDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SubheadingName { get; set; }
        public string SubheadingOfSubheadingName { get; set; }
        public string AuthorName { get; set; }
        public string LanguageName { get; set; }
        public string PublisherName { get; set; }
        public string BookName { get; set; }
        public int Price { get; set; }
        public int StockQty { get; set; }
        public string Explanation { get; set; }
    }
}
