using AutoMapper;
using DataParserService.DataParser;
using DataParserService.Dtos;
using DataParserService.Models;
using DataParserService.RabbitMQ;
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
        private readonly IParser _parser;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMapper _mapper;

        public Repository(AppDbContext context, IMessageBusClient messageBusClient, IMapper mapper)
        {
            _context = context;
            _messageBusClient = messageBusClient;
            _mapper = mapper;
            _parser = new Parser();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        #region Company

        public void AddCompany(Company company)
        {
            if (company != null)
            {
                _context.Companies.Add(company);
                _context.SaveChanges();

                Console.WriteLine($"--> Added company: {company.Name}");

                try
                {
                    var companyPublishedDto = _mapper.Map<CompanyPublishedDto>(company);
                    companyPublishedDto.Event = "Company_Published";
                    _messageBusClient.Publish(companyPublishedDto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"--> Couldn't add company: {company.Name}");
            }
        }

        public bool IsUpdateMultiplicatorsForCompany(int companyId)
        {
            return !_context.Multiplicators.Any() || _context.Companies.FirstOrDefault(f => f.Id == companyId)?.LastMultiplicatorsUpdate.Day != DateTime.Now.Day;
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
            var company = _context.Companies.FirstOrDefault(f => f.SecuritieTQBR.SECID == secId);

            if (company != null)
            {
                company.SecuritieTQBR = GetSecuritieTQBRById(company.SecuritieTQBRId);
            }
            else
            {
                Console.WriteLine($"--> Couldn't find company by SecId: {secId}");
            }

            return company;
        }

        public Company GetCompanyById(int id)
        {
            var company = _context.Companies.FirstOrDefault(f => f.Id == id);

            if (company != null)
            {
                company.SecuritieTQBR = GetSecuritieTQBRById(company.SecuritieTQBRId);
            }
            else
            {
                Console.WriteLine($"--> Couldn't find company by Id: {id}");
            }

            return company;
        }

        #endregion

        #region Multiplicator

        public void AddMultiplicatorForCompany(int companyId, Multiplicator multiplicator)
        {
            if (multiplicator == null) throw new ArgumentNullException(nameof(multiplicator));

            var company = _context.Companies.FirstOrDefault(f => f.Id == companyId);
            multiplicator.CompanyId = companyId;

            try
            {
                var multiplicatorPublishedDto = _mapper.Map<MultiplicatorPublishedDto>(multiplicator);
                multiplicatorPublishedDto.SecId = GetCompanyById(companyId)?.SecuritieTQBR?.SECID;
                multiplicatorPublishedDto.Event = "Multiplicators_Published";
                _messageBusClient.Publish(multiplicatorPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            _context.Indexes.AddRange(multiplicator.Indexes);
            _context.Multiplicators.Add(multiplicator);
            _context.SaveChanges();
        }

        public void RemoveMultiplicatorsForCompany(int companyId)
        {
            foreach (var multiplicator in _context.Multiplicators.Where(w => w.CompanyId == companyId).ToList())
            {
                foreach (var index in _context.Indexes.Where(w => w.MultiplicatorId == multiplicator.Id).ToList())
                {
                    _context.Indexes.Remove(index);
                }

                _context.Multiplicators.Remove(multiplicator);
            }

            _context.SaveChanges();
        }

        public IEnumerable<Multiplicator> UpdateMultiplicatorsForCompany(int companyId)
        {
            var multiplicators = new List<Multiplicator>();

            RemoveMultiplicatorsForCompany(companyId);

            foreach (var multiplicator in _parser.ParseCompanyAllMultiplicators(GetCompanyById(companyId)))
            {
                AddMultiplicatorForCompany(companyId, multiplicator);

                multiplicators.Add(multiplicator);
            }

            return multiplicators;
        }

        public IEnumerable<Multiplicator> GetMultiplicatorsForCompany(int companyId)
        {
            var multiplicators = _context.Multiplicators
                .Where(w => w.CompanyId == companyId)
                .OrderBy(o => o.Company.Name);

            foreach (var multiplicator in multiplicators.ToList())
            {
                multiplicator.Indexes = _context.Indexes.Where(w => w.MultiplicatorId == multiplicator.Id).ToList();
            }

            return multiplicators;
        }

        #endregion

        #region Securitie TQBR
        public void InitSecuritiesTQBR()
        {
            _context.SecuritiesTQBR.RemoveRange(_context.SecuritiesTQBR);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=securities&securities.columns=SECID,SECTYPE");
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

        public SecuritieTQBR GetSecuritieTQBRById(int id)
        {
            return _context.SecuritiesTQBR.FirstOrDefault(f => f.Id == id);
        }

        public SecuritieTQBR GetSecuritieTQBRBySecId(string secId)
        {
            return _context.SecuritiesTQBR.FirstOrDefault(f => f.SECID == secId);
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
