using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Dtos
{
    public class DbListNodeDto
    {
        public int Id { get; set; }
        public string[] Keys { get; set; }
        public string[] Values { get; set; }
    }
}
