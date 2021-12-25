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
            multiplicatorModel.Name = multiplicator.Name;
            multiplicatorModel.Indexes = new List<Models.Index>();

            for (int i = 0; i < multiplicator.IndexKey.Count; i++)
            {
                multiplicatorModel.Indexes.Add(new Models.Index()
                {
                    Key = multiplicator.IndexKey.ToList()[i],
                    Value = multiplicator.IndexValue.ToList()[i],
                    MultiplicatorId = multiplicatorModel.Id
                });
            }

            _repository.AddMultiplicatorForCompany(companyId, multiplicatorModel);
            _repository.SaveChanges();

            var multiplicators = _mapper.Map<List<MultiplicatorReadDto>>(_repository.GetMultiplicatorsForCompany(companyId));

            return CreatedAtRoute(nameof(GetMultiplicatorsForCompany), new { companyId = companyId }, multiplicators);
        }

        [HttpPost("Update/{companyId}", Name = "UpdateMultiplicatorsForCompany")]
        public ActionResult<List<MultiplicatorReadDto>> UpdateMultiplicatorsForCompany(int companyId)
        {
            Console.WriteLine($"--> Hit UpdateMultiplicatorsForCompany: {companyId}");

            var multiplicators = _mapper.Map<List<MultiplicatorReadDto>>(_repository.UpdateMultiplicatorsForCompany(companyId));

            if (multiplicators.Count > 0)
            {
                Console.WriteLine($"--> Updated Multiplicators For Company: {companyId}");
            }
            else
            {
                Console.WriteLine($"--> Couldn't Update Multiplicators For Company: {companyId}");
            }

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

            var multiplicatorsReadDto = _mapper.Map<List<MultiplicatorReadDto>>(multiplicators);

            return Ok(multiplicatorsReadDto);
        }

    }
}
