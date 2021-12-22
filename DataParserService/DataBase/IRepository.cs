using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.DataBase
{
    public interface IRepository
    {
        bool SaveChanges();

        // Multiplicators.
        void AddMultiplicatorForCompany(int companyId, Multiplicator multiplicator);
        IEnumerable<Multiplicator> GetMultiplicatorsForCompany(int companyId);

        // Companies.
        IEnumerable<Company> GetAllCompanies();
        Company GetCompanyBySecId(string secId);
        void AddCompany(Company company);
        bool IsCompanyExists(SecuritieTQBR securitieTQBR);

        // Securities TQBR.
        void InitSecuritiesTQBR();
        IEnumerable<SecuritieTQBR> GetSecuritiesTQBR();
        bool IsUpdateSecuritiesTQBR();
        bool IsSecuritieTQBRExists(string secId);
    }
}
