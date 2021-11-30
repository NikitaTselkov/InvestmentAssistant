using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Models
{
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Sector { get; set; }

        [Required]
        public int YearFoundation { get; set; }

        [Required]
        public ICollection<Multiplicator> Multiplicators { get; set; }
    }
}
