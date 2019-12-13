using System;
using System.Collections.Generic;

namespace DividendApi.Models
{
    public partial class Stock
    {
        public Stock()
        {
            Dividend = new HashSet<Dividend>();
        }

        public int StockId { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Shares { get; set; }
        public double Price { get; set; }
        public double Fees { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Dividend> Dividend { get; set; }
    }
}
