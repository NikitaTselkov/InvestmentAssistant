using DataParserService.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataParserService.DataBase
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        #region Company

        public void AddCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            _context.Companies.Add(company);
            _context.SaveChanges();

            Console.WriteLine($"--> Added company: {company.Name}");
        }

        public bool IsCompanyExists(SecuritieTQBR securitieTQBR)
        {
            return _context.Companies.FirstOrDefault(f => f.SecuritieTQBR == securitieTQBR) != null;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company GetCompanyBySecId(string secId)
        {
            return _context.Companies.FirstOrDefault(f => f.SecuritieTQBR.SECID == secId);
        }

        #endregion

        #region Multiplicator

        public void AddMultiplicatorForCompany(int companyId, Multiplicator multiplicator)
        {
            if (multiplicator == null) throw new ArgumentNullException(nameof(multiplicator));

            var company = _context.Companies.FirstOrDefault(f => f.Id == companyId);
            multiplicator.CompanyId = companyId;

            _context.Multiplicators.Add(multiplicator);
            _context.SaveChanges();
        }

        public IEnumerable<Multiplicator> GetMultiplicatorsForCompany(int companyId)
        {
            return _context.Multiplicators
                .Where(w => w.CompanyId == companyId)
                .OrderBy(o => o.Company.Name);
        }

        #endregion

        #region Securitie TQBR
        public void InitSecuritiesTQBR()
        {
            _context.SecuritiesTQBR.RemoveRange(_context.SecuritiesTQBR);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=securities&securities.columns=SECID,SECTYPE");
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    var tokens = response.Content.ReadAsAsync<JToken>().Result;

                    foreach (var token in tokens["securities"]["data"])
                    {
                        _context.SecuritiesTQBR.Add(new SecuritieTQBR()
                        {
                            SECID = token[0].ToString(),
                            SECTYPE = token[1].ToString(),
                            LastUpdate = DateTime.Now
                        });;
                    }

                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
        public IEnumerable<SecuritieTQBR> GetSecuritiesTQBR()
        {
            return _context.SecuritiesTQBR.ToList();
        }

        public bool IsUpdateSecuritiesTQBR()
        {
            return !_context.SecuritiesTQBR.Any() || _context.SecuritiesTQBR?.OrderBy(o => o).FirstOrDefault()?.LastUpdate.Day == DateTime.Now.Day - 7; // Обновляет раз в неделю.
        }

        public bool IsSecuritieTQBRExists(string secId)
        {
            return _context.SecuritiesTQBR.Any(a => a.SECID == secId);
        }

        #endregion
    }
}
