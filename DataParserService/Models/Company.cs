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
        public string SectorLongName { get; set; }

        [Required]
        public string SectorShortName { get; set; }

        [Required]
        public ICollection<Multiplicator> Multiplicators { get; set; }

        [Required]
        public int SecuritieTQBRId { get; set; }

        public SecuritieTQBR SecuritieTQBR { get; set; }

        private IParser _parser = new Parser();

        public Company() { }

        public Company(SecuritieTQBR securitieTQBR)
        {
            var (sectorLongName, sectorShortName) = _parser.ParseCompanySector(securitieTQBR);

            Name = _parser.ParseCompanyName(securitieTQBR);
            SecuritieTQBR = securitieTQBR;
            SectorLongName = sectorLongName;
            SectorShortName = sectorShortName;
            Multiplicators = _parser.ParseCompanyAllMultiplicators(this);
        }

        public void UpdateMultiplicators()
        {
            Multiplicators = _parser.ParseCompanyAllMultiplicators(this);
        }
    }
}
