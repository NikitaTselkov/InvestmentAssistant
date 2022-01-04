using DataParserService.DataBase;
using DataParserService.DataParser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Models
{
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public string Sector { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime LastMultiplicatorsUpdate { get; set; }

        [Required]
        public ICollection<Multiplicator> Multiplicators { get; set; }

        [Required]
        public int SecuritieTQBRId { get; set; }

        public SecuritieTQBR SecuritieTQBR { get; set; }

        private IParser _parser = new Parser();

        public Company() { }

        public Company(SecuritieTQBR securitieTQBR)
        {
            var secId = securitieTQBR.SECID;
            var (industry, sector) = _parser.ParseCompanySector(secId);

            Name = _parser.ParseCompanyName(secId);
            SecuritieTQBR = securitieTQBR;
            Industry = industry;
            Sector = sector;
            Country = _parser.ParseCompanyCountry(secId);
            LastMultiplicatorsUpdate = DateTime.Now;
            Multiplicators = _parser.ParseCompanyAllMultiplicators(this);
        }
    }
}
