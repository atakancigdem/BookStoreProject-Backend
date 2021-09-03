using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Language : IEntity
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
    }
}
