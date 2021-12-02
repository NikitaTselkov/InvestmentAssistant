using AutoMapper;
using DataParserService.DataBase;
using DataParserService.Dtos;
using DataParserService.Models;
using DataParserService.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Controllers
{
    [Route("api/dataparser/[controller]")]
    [ApiController]
    public class MultiplicatorsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public MultiplicatorsController(IRepository repository, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpPost("{companyId}")]
        public ActionResult<List<MultiplicatorReadDto>> AddMultiplicatorForCompany(int companyId, MultiplicatorCreateDto multiplicator)
        {
            var multiplicatorModel = _mapper.Map<Multiplicator>(multiplicator);

            _repository.AddMultiplicatorForCompany(companyId, multiplicatorModel);
            _repository.SaveChanges();

            var multiplicators = _mapper.Map<List<MultiplicatorReadDto>>(_repository.GetMultiplicatorsForCompany(companyId));

            return CreatedAtRoute(nameof(GetMultiplicatorsForCompany), new { companyId = companyId }, multiplicators);
        }

        [HttpGet("{companyId}", Name = "GetMultiplicatorsForCompany")]
        public ActionResult<List<MultiplicatorReadDto>> GetMultiplicatorsForCompany(int companyId)
        {
            Console.WriteLine($"--> Hit GetMultiplicatorsForCompany: {companyId}");

            var multiplicators = _repository.GetMultiplicatorsForCompany(companyId);

            if (multiplicators == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<MultiplicatorReadDto>>(multiplicators));
        }

    }
}
