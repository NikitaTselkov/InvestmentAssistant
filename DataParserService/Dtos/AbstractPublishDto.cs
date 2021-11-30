using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public abstract class AbstractPublisedhDto
    {
        public int Id { get; set; }
        public string Event { get; set; }
    }
}
