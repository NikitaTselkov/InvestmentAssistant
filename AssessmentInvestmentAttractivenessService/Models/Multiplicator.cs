using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Models
{
    public class Multiplicator
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string MultiplicatorName { get; set; }

        [Required]
        public string GroupOfMultiplicators { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public List<string> DoesNotWorkWithCompanies { get; set; }
        
        [Required]
        public bool IfNeedToConsiderTheDynamics { get; set; }

        [Required]
        public ICollection<Index> Indexes { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
