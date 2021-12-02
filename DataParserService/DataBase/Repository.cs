using DataParserService.IssMoexApi.Models;
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
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company GetCompanyById(int id)
        {
            return _context.Companies.FirstOrDefault(f => f.Id == id);
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=securities&securities.columns=SECID,SHORTNAME,SECNAME,SECTYPE");
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
                            SHORTNAME = token[1].ToString(),
                            SECNAME = token[2].ToString(),
                            SECTYPE = token[3].ToString()
                        });
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

        public bool IsSecuritiesTQBRContainsAny()
        {
            return _context.SecuritiesTQBR.Any();
        }

        #endregion

        #region Capitalization of company.

        public void CalculateCapitalizations()
        {
            CapitalizationCompany capitalizationCompany = null;
            Stock stock = null;
            int lastIndex = 0;
            var securities = GetSecuritiesTQBR().OrderBy(o => o.SECID).ToList();
            
            for (int i = 0; i < securities.Count; i++)
            {
                try
                {
                    stock = GetStockPrice(securities[i]);
                    capitalizationCompany = new CapitalizationCompany()
                    {
                        SECID = stock.SECID,
                        CAPITALIZATION = stock.ISSUESIZE * stock.PRICE,
                        DATA = stock.PREVDATE
                    };

                    if (i + 1 < securities.Count)
                    {
                        lastIndex = securities[i + 1].SECID.Length - 1;

                        if (securities[i].SECID == securities[i + 1].SECID.Remove(lastIndex))
                        {
                            stock = GetStockPrice(securities[i + 1]);
                            capitalizationCompany.CAPITALIZATION += stock.ISSUESIZE * stock.PRICE;
                        }
                    }

                    _context.CapitalizationCompanies.Add(capitalizationCompany);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"--> {e.Message}");
                }
            }

            _context.SaveChanges();
        }

        public CapitalizationCompany GetCapitalization(SecuritieTQBR securitieTQBR)
        {
            return _context.CapitalizationCompanies.FirstOrDefault(f => f.SECID == securitieTQBR.SECID);
        }

        public DateTime? GetLastUpdateCapitalizations()
        {
            return _context.CapitalizationCompanies.FirstOrDefault()?.DATA;
        }

        public void DeleteAllCapitalizations()
        {
            _context.CapitalizationCompanies = null;
            _context.SaveChanges();
        }

        public Stock GetStockPrice(SecuritieTQBR securitieTQBR)
        {
            if (securitieTQBR.SECTYPE == "1" || securitieTQBR.SECTYPE == "2")
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{securitieTQBR.SECID}.json?iss.meta=off&iss.only=securities&securities.columns=SECID,ISSUESIZE,PREVPRICE,PREVDATE");
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var tokens = response.Content.ReadAsAsync<JToken>().Result;
                        var token = tokens["securities"]["data"];

                        var secId = token[0][0].ToString();

                        return new Stock
                        (
                            secId: token[0][0].ToString(),
                            issueSize: (long)token[0][1],
                            price: (double)token[0][2],
                            prevDate: (DateTime)token[0][3]
                        );
                    }
                    else
                    {
                        throw new Exception($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                    }
                }
            }
            else
            {
                throw new Exception($"{securitieTQBR.SECID} --> Stock SECTYPE: {securitieTQBR.SECTYPE} is not supported");
            }
        }

        #endregion
    }
}
