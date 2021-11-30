using DataParserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.RabbitMQ
{
    public interface IMessageBusClient
    {
        void Publish(AbstractPublisedhDto publishedDto);
    }
}
