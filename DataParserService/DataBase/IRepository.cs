using DataParserService.IssMoexApi.Models;
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
        Company GetCompanyById(int id);
        void AddCompany(Company company);

        // Securities TQBR.
        void InitSecuritiesTQBR();
        IEnumerable<SecuritieTQBR> GetSecuritiesTQBR();
        bool IsSecuritiesTQBRContainsAny();

        // Capitalization of companies.

        void CalculateCapitalizations();
        CapitalizationCompany GetCapitalization(SecuritieTQBR securitieTQBR);
        void DeleteAllCapitalizations();
        DateTime? GetLastUpdateCapitalizations();

        Stock GetStockPrice(SecuritieTQBR securitieTQBR);
      
    }
}
