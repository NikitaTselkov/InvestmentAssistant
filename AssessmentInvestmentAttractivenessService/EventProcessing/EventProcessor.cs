using AssessmentInvestmentAttractivenessService.DataBase;
using AssessmentInvestmentAttractivenessService.Dtos;
using AssessmentInvestmentAttractivenessService.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.EventProcessing
{
    enum EventType
    {
        CompanyPublished,
        MultiplicatorsPublished,
        Undetermined
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.CompanyPublished:
                    AddCompany(message);
                    break;
                case EventType.MultiplicatorsPublished:
                    AddMultiplicators(message);
                    break;
                case EventType.Undetermined:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Company_Published":
                    Console.WriteLine("--> Company Published Event Detected");
                    return EventType.CompanyPublished;
                case "Multiplicators_Published":
                    Console.WriteLine("--> Multiplicator Published Event Detected");
                    return EventType.MultiplicatorsPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void AddCompany(string publishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IRepository>();
                var publishedDto = JsonSerializer.Deserialize<CompanyPublishedDto>(publishedMessage);

                try
                {
                    var company = _mapper.Map<Company>(publishedDto);
                    if (!repo.CompanyExists(company.SecId))
                    {
                        repo.AddCompany(company);
                        Console.WriteLine($"--> Added company: {company.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"--> Company: {company.Name} already exists...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Company to DB {ex.Message}");
                }
            }
        }

        private void AddMultiplicators(string publishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IRepository>();
                var publishedDto = JsonSerializer.Deserialize<MultiplicatorPublishedDto>(publishedMessage);

                publishedDto.Name = publishedDto.Name.ToUpper().Replace("_", "/");

                try
                {
                    var company = repo.GetCompanyBySecId(publishedDto.SecId);
                    var multiplicator = _mapper.Map<Multiplicator>(publishedDto);
                    var indexes = new List<Models.Index>();

                    multiplicator.DescriptionId = repo.GetDescriptionForMultiplicators(publishedDto.Name).Id;               

                    for (int i = 0; i < publishedDto.IndexKey.Count; i++)
                    {
                        indexes.Add(new Models.Index()
                        {
                            Key = publishedDto.IndexKey.ToList()[i],
                            Value = publishedDto.IndexValue.ToList()[i],
                            MultiplicatorId = multiplicator.Id
                        });
                    }

                    if (repo.MultiplicatorsForCompanyExists(publishedDto.SecId, publishedDto.Name))
                    {
                        repo.RemoveMultiplicatorsForCompany(publishedDto.SecId);
                    }

                    repo.AddMultiplicatorForCompany(publishedDto.SecId, multiplicator);
                    repo.AddIndexesForMultiplicator(multiplicator.Id, indexes);
                    Console.WriteLine($"--> Added multiplicator {publishedDto.Name} for company: {company.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Multiplicators to DB {ex.Message}");
                }
            }
        }
    }
}
