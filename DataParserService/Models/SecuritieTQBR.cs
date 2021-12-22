using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Models
{
    [Table("Securities_TQBR")]
    public class SecuritieTQBR
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [JsonProperty]
        public string SECID { get; set; }
       
        [Required]
        [JsonProperty]
        public string SECTYPE { get; set; }

        public Company Company { get; set; }
    }
}
