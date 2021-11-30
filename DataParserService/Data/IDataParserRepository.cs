using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Data
{
    public interface IDataParserRepository
    {
        bool SaveChanges();

        // Multiplicators.
        void AddMultiplicatorForCompany(int companyId, Multiplicator multiplicator);
        IEnumerable<Multiplicator> GetMultiplicatorsForCompany(int companyId);

        // Companies.
        IEnumerable<Company> GetAllCompanies();
        Company GetCompanyById(int id);
        void AddCompany(Company company);
    }
}
