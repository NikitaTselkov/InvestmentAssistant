using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Models
{
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public string Sector { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string SecId { get; set; }

        [Required]
        public ICollection<Multiplicator> Multiplicators { get; set; }
    }
}
