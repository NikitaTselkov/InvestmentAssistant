using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public class CompanyReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public int YearFoundation { get; set; }
    }
}
