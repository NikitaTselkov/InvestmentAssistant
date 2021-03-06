using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Models
{
    public class DescriptionForMultiplicators
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string HowToInterpret { get; set; }
        
        [Required]
        public string CodeOfGroupOfMultiplicator { get; set; }
        
        public GroupOfMultiplicators GroupOfMultiplicators { get; set; }

        [Required]
        public ICollection<Multiplicator> Multiplicators { get; set; }
    }
}
