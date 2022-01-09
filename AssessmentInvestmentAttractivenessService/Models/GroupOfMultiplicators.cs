using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Models
{
    public class GroupOfMultiplicators
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string GroupCode { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public ICollection<DescriptionForMultiplicators> DescriptionsForMultiplicators { get; set; }
    }
}
