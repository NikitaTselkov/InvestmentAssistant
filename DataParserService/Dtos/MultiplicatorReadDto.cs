using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public class MultiplicatorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> IndexKey { get; set; }
        public ICollection<double> IndexValue { get; set; }
        public int CompanyId { get; set; }
    }
}
