using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models
{
    public class TradingQueryViewModel
    {
        public DateTime? TradeDate_S { get; set; }
        public DateTime? TradeDate_E { get; set; }
        public string Buyer { get; set; }
        public string CommodityID { get; set; }
        public List<string> TransNos { get; set; }
        public bool? IsShipped { get; set; }
        public string ShipperNo { get; set; }
    }
}