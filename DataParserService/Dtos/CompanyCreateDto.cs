using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public class CompanyCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Sector { get; set; }

        [Required]
        public int YearFoundation { get; set; }

        public List<Multiplicator> Multiplicators { get; set; }
    }
}
