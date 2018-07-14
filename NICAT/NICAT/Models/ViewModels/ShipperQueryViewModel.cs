using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models
{
    public class ShipperQueryViewModel
    {
        public DateTime? TradeDate_S { get; set; }
        public DateTime? TradeDate_E { get; set; }
        public string Buyer { get; set; }
    }
}