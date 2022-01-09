using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Dtos
{
    public class MultiplicatorPublishedDto : AbstractPublishedDto
    {
        public string Name { get; set; }
        public ICollection<string> IndexKey { get; set; }
        public ICollection<double> IndexValue { get; set; }
        public string SecId { get; set; }
    }
}
