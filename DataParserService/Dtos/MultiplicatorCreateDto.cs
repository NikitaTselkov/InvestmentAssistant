using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Dtos
{
    public class MultiplicatorCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<string> IndexKey { get; set; }

        [Required]
        public ICollection<double> IndexValue { get; set; }
    }
}
