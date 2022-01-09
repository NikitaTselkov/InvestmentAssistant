using AssessmentInvestmentAttractivenessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.DataBase
{
    public interface IRepository
    {
        // Indexes.
        void AddIndexesForMultiplicator(int multiplicatorId, List<Models.Index> indexes);

        // Multiplicators.
        void AddMultiplicatorForCompany(string secId, Multiplicator multiplicator);
        void RemoveMultiplicatorsForCompany(string secId);
        IEnumerable<Multiplicator> GetMultiplicatorsForCompany(string secId);
        Multiplicator GetMultiplicatorById(int multiplicatorId);
        bool MultiplicatorsForCompanyExists(string secId, string multiplicatorsName);
        DescriptionForMultiplicators GetDescriptionForMultiplicators(string multiplicatorsName);

        // Companies.
        IEnumerable<Company> GetAllCompanies();
        Company GetCompanyBySecId(string secId);
        bool CompanyExists(string secId);
        void AddCompany(Company company);
    }
}
