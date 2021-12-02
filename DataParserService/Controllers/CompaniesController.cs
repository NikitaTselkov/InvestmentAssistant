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

            var companyItem = _repository.GetAllCompanies();

            return Ok(_mapper.Map<IEnumerable<CompanyReadDto>>(companyItem));
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public ActionResult<CompanyReadDto> GetCompanyById(int id)
        {
            Console.WriteLine("--> Getting Company by Id...");

            var companyItem = _repository.GetCompanyById(id);

            if (companyItem == null) return NotFound();       

            return Ok(_mapper.Map<CompanyReadDto>(companyItem));
        }

        [HttpPost]
        public ActionResult<CompanyReadDto> AddCompany(CompanyCreateDto companyCreateDto)
        {
            Console.WriteLine("--> Addeting Company...");

            var companyModel = _mapper.Map<Company>(companyCreateDto);
            _repository.AddCompany(companyModel);
            _repository.SaveChanges();

            var companyReadDto = _mapper.Map<CompanyReadDto>(companyModel);

            try
            {
                var companyPublishedDto = _mapper.Map<CompanyPublishedDto>(companyReadDto);
                companyPublishedDto.Event = "CompanyPublished";
                _messageBusClient.Publish(companyPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetCompanyById), new { id = companyReadDto.Id }, companyReadDto);
        }
    }
}
