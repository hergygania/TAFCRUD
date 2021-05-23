using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspiro.Models
{
    public class TransactionModel
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public int Code { get; set; }
        public string Brand { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Qty { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string Bulan { get; set; }
        public string year { get; set; }
    }
}
