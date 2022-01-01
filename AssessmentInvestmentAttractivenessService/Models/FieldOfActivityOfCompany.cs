using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Models
{
    public class FieldOfActivityOfCompany
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string FieldOfActivityCode { get; set; }
        
        [Required]
        public string FieldOfActivityName { get; set; }
    }
}
