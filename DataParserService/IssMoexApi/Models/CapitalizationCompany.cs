using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.IssMoexApi.Models
{
    [Table("Capitalization_Of_Companies")]
    public class CapitalizationCompany
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [JsonProperty]
        public string SECID { get; set; }
        
        [Required]
        [JsonProperty]
        public double CAPITALIZATION { get; set; }

        [Required]
        [JsonProperty]
        public DateTime DATA { get; set; }
    }
}
