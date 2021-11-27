using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Data
{
    public class DataParserRepository : IDataParserRepository
    {
        private readonly AppDbContext _context;

        public DataParserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddPriceIndex(PriceIndex priceIndex)
        {
            if (priceIndex == null) throw new ArgumentNullException(nameof(priceIndex));

            _context.PriceIndices.Add(priceIndex);
        }

        public void DeletePriceIndex(int id)
        {
            var priceIndex = GetPriceIndexById(id);
            if (priceIndex == null) throw new ArgumentNullException(nameof(priceIndex), $"PriceIndex c Id = {id} не существует");

            _context.PriceIndices.Remove(priceIndex);
        }

        public IEnumerable<PriceIndex> GetAllPriceIndeces()
        {
            return _context.PriceIndices.ToList();
        }

        public PriceIndex GetPriceIndexById(int id)
        {
            return _context.PriceIndices.FirstOrDefault(f => f.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
