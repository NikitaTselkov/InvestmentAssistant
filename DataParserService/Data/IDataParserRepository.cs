using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Data
{
    public interface IDataParserRepository
    {
        bool SaveChanges();
        IEnumerable<PriceIndex> GetAllPriceIndeces();
        PriceIndex GetPriceIndexById(int id);
        void AddPriceIndex(PriceIndex priceIndex);
        void DeletePriceIndex(int id);
    }
}
