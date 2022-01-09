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
        public ICollection<Index> Indexes { get; set; }

        public ICollection<FieldOfActivityOfCompany> DoesNotWorkWithCompanies { get; set; }

        [Required]
        public int DescriptionId { get; set; }
        public DescriptionForMultiplicators Description { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
