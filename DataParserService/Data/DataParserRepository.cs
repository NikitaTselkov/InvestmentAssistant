using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Data
{
    public class DataParserRepository : IDataParserRepository
    {
        private readonly AppDbContext _context;

        public DataParserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));
            
            _context.Companies.Add(company);
        }

        public void AddMultiplicatorForCompany(int companyId, Multiplicator multiplicator)
        {
            if (multiplicator == null) throw new ArgumentNullException(nameof(multiplicator));

            var company = _context.Companies.FirstOrDefault(f => f.Id == companyId);

            multiplicator.CompanyId = companyId;

            _context.Multiplicators.Add(multiplicator);
        }

        public IEnumerable<Multiplicator> GetMultiplicatorsForCompany(int companyId)
        {
            return _context.Multiplicators
                .Where(w => w.CompanyId == companyId)
                .OrderBy(o => o.Company.Name);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company GetCompanyById(int id)
        {
            return _context.Companies.FirstOrDefault(f => f.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
