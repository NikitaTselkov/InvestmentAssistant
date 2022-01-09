using AutoMapper;
using DataParserService.DataBase;
using DataParserService.Dtos;
using DataParserService.Models;
using DataParserService.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataParserService.Controllers
{
    [Route("api/dataparser/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public CompaniesController(IRepository repository, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyReadDto>> GetAllCompanies()
        {
            Console.WriteLine("--> Getting Companies...");

            var companies = _repository.GetAllCompanies();
            var companiesReadDto = new List<CompanyReadDto>();
            var companyReadDto = new CompanyReadDto();

            foreach (var company in companies)
            {
                companyReadDto = _mapper.Map<CompanyReadDto>(company);
                companyReadDto.SecId = _repository.GetSecuritieTQBRById(company.SecuritieTQBRId).SECID;
                companiesReadDto.Add(companyReadDto);
            }

            return Ok(companiesReadDto);
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public ActionResult<CompanyReadDto> GetCompanyById(int id)
        {
            Console.WriteLine("--> Getting Company by Id...");

            Company companyItem = _repository.GetCompanyById(id);

            if (companyItem == null) return NotFound();       

            var companyReadDto = _mapper.Map<CompanyReadDto>(companyItem);
            companyReadDto.SecId = companyItem.SecuritieTQBR.SECID;

            return Ok(companyReadDto);
        }

        [HttpPost]
        public ActionResult<CompanyReadDto> AddCompany(CompanyCreateDto companyCreateDto)
        {
            Console.WriteLine("--> Addeting Company...");

            if (companyCreateDto == null || !_repository.GetSecuritiesTQBR().Any(a => a.SECID == companyCreateDto.SecId)) return NotFound();

            var company = new Company(_repository.GetSecuritieTQBRBySecId(companyCreateDto.SecId));
            var companyReadDto = _mapper.Map<CompanyReadDto>(company);
            company.SecuritieTQBRId = company.SecuritieTQBR.Id;
            companyReadDto.SecId = company.SecuritieTQBR.SECID;

            _repository.AddCompany(company);

            companyReadDto.Id = company.Id;

            return CreatedAtRoute(nameof(GetCompanyById), new { id = companyReadDto.Id }, companyReadDto);
        }
    }
}
