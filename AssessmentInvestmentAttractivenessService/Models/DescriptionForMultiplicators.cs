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
        public string Description { get; set; }
    }
}
