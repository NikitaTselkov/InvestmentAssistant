using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public class MultiplicatorPublishedDto : AbstractPublisedhDto
    {
        public string Name { get; set; }
        public double Index { get; set; }
        public int CompanyId { get; set; }
    }
}
