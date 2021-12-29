using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Models
{
    public class Index
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public int MultiplicatorId { get; set; }

        public Multiplicator Multiplicator { get; set; }
    }
}
