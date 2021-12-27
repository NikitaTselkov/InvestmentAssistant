using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public class CompanyPublishedDto : AbstractPublisedhDto
    {
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public string Country { get; set; }
        public string SecId { get; set; }
    }
}
