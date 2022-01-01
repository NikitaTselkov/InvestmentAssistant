using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Models
{
    public class GroupOfMultiplicators
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string GroupCode { get; set; }

        [Required]
        public string GroupName { get; set; }
    }
}
