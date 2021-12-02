using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.IssMoexApi.Models
{
    public class Stock
    {
        public string SECID { get; set; }
        public double PRICE { get; set; }
        public long ISSUESIZE { get; set; }
        public DateTime PREVDATE { get; set; }

        public Stock(string secId, double price, long issueSize, DateTime prevDate)
        {
            SECID = secId;
            PRICE = price;
            ISSUESIZE = issueSize;
            PREVDATE = prevDate;
        }
    }
}
