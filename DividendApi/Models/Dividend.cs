using System;
using System.Collections.Generic;

namespace DividendApi.Models
{
    public partial class Dividend
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string ExDate { get; set; }
        public string PaymentDate { get; set; }
        public string RecordDate { get; set; }
        public string DeclaredDate { get; set; }
        public double Amount { get; set; }
        public string Flag { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string Frequency { get; set; }
        public int? StockId { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
