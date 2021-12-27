using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.DataParser
{
    public interface IParser
    {
        List<Multiplicator> ParseCompanyAllMultiplicators(Company company);
        Multiplicator ParseCompanyMultiplicator(Company company, string multiplicatorName);
        string ParseCompanyName(string secId);
        string ParseCompanyCountry(string secId);
        (string industry, string sector) ParseCompanySector(string secId);
    }
}
