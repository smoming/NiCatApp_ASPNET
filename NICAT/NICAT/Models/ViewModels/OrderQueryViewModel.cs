using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models
{
    public class OrderQueryViewModel
    {
        public DateTime? TradeDate_S { get; set; }
        public DateTime? TradeDate_E { get; set; }
        public string CommodityID { get; set; }
        public List<string> TransNos { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsPurchased { get; set; }
        public string ReceiptNo { get; set; }
        public string PurchaseNo { get; set; }
    }
}