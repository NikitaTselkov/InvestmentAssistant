using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public class MultiplicatorPublishedDto : AbstractPublishDto
    {
        public string Name { get; set; }
        public ICollection<string> IndexKey { get; set; }
        public ICollection<double> IndexValue { get; set; }
        public string SecId { get; set; }
    }
}
