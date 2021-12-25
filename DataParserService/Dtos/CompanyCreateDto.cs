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
        public string SecId { get; set; }
    }
}
