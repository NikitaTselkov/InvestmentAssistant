using AssessmentInvestmentAttractivenessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.DataBase
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        public void AddMultiplicatorForCompany(string secId, Multiplicator multiplicator)
        {
            if (string.IsNullOrWhiteSpace(secId)) throw new ArgumentNullException(nameof(secId));
            if (multiplicator == null) throw new ArgumentNullException(nameof(multiplicator));

            var company = GetCompanyBySecId(secId);

            if (company == null) throw new ArgumentNullException(nameof(company));

            multiplicator.CompanyId = company.Id;

            _context.Multiplicators.Add(multiplicator);
            _context.SaveChanges();
        }

        public void RemoveMultiplicatorsForCompany(string secId)
        {
            if (string.IsNullOrWhiteSpace(secId)) throw new ArgumentNullException(nameof(secId));

            foreach (var multiplicator in GetMultiplicatorsForCompany(secId)?.ToList())
            {
                foreach (var index in _context.Indexes.Where(w => w.MultiplicatorId == multiplicator.Id).ToList())
                {
                    _context.Indexes.Remove(index);
                }

                _context.Multiplicators.Remove(multiplicator);
            }

            _context.SaveChanges();
        }

        public Multiplicator GetMultiplicatorById(int multiplicatorId)
        {
            return _context.Multiplicators.OrderBy(o => o).FirstOrDefault(f => f.Id == multiplicatorId);
        }

        public void AddIndexesForMultiplicator(int multiplicatorId, List<Models.Index> indexes)
        {
            if (indexes == null) throw new ArgumentNullException(nameof(indexes));

            var multiplicator = GetMultiplicatorById(multiplicatorId);

            if (multiplicator == null) throw new ArgumentNullException(nameof(multiplicator));

            multiplicator.Indexes = indexes;

            _context.Indexes.AddRange(multiplicator.Indexes);
            _context.SaveChanges();
        }

        public DescriptionForMultiplicators GetDescriptionForMultiplicators(string multiplicatorsName)
        {
            if (string.IsNullOrWhiteSpace(multiplicatorsName)) throw new ArgumentNullException(nameof(multiplicatorsName));

            return _context.DescriptionsForMultiplicators.OrderBy(o => o).FirstOrDefault(f => f.Name == multiplicatorsName);
        }

        public bool CompanyExists(string secId)
        {
            if (string.IsNullOrWhiteSpace(secId)) throw new ArgumentNullException(nameof(secId));

            return _context.Companies.Any(a => a.SecId == secId);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company GetCompanyBySecId(string secId)
        {
            if (string.IsNullOrWhiteSpace(secId)) throw new ArgumentNullException(nameof(secId));

            return _context.Companies.OrderBy(o => o).FirstOrDefault(f => f.SecId == secId);
        }

        public IEnumerable<Multiplicator> GetMultiplicatorsForCompany(string secId)
        {
            if (string.IsNullOrWhiteSpace(secId)) throw new ArgumentNullException(nameof(secId));

            var company = GetCompanyBySecId(secId);

            return _context.Multiplicators.Where(w => w.CompanyId == company.Id);
        }

        public bool MultiplicatorsForCompanyExists(string secId, string multiplicatorsName)
        {
            if (string.IsNullOrWhiteSpace(secId)) throw new ArgumentNullException(nameof(secId));
            if (string.IsNullOrWhiteSpace(multiplicatorsName)) throw new ArgumentNullException(nameof(multiplicatorsName));

            var company = GetCompanyBySecId(secId);

            return _context.Multiplicators.Any(f => f.CompanyId == company.Id && f.Description.Name == multiplicatorsName);
        }
    }
}
