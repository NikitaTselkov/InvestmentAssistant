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
        void RemoveMultiplicatorsForCompany(int companyId);
        IEnumerable<Multiplicator> GetMultiplicatorsForCompany(int companyId);
        IEnumerable<Multiplicator> UpdateMultiplicatorsForCompany(int companyId);

        // Companies.
        IEnumerable<Company> GetAllCompanies();
        Company GetCompanyBySecId(string secId);
        Company GetCompanyById(int id);
        void AddCompany(Company company);
        bool IsUpdateMultiplicatorsForCompany(int companyId);
        bool IsCompanyExists(SecuritieTQBR securitieTQBR);

        // Securities TQBR.
        void InitSecuritiesTQBR();
        IEnumerable<SecuritieTQBR> GetSecuritiesTQBR();
        SecuritieTQBR GetSecuritieTQBRById(int id);
        SecuritieTQBR GetSecuritieTQBRBySecId(string secId);
        bool IsUpdateSecuritiesTQBR();
        bool IsSecuritieTQBRExists(string secId);
    }
}
