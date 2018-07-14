using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models.ApiModel
{
    public class ShipperDTOs
    {
        public string TransNo { get; set; }
        public System.DateTime TradeDate { get; set; }
        public string Buyer { get; set; }
        public decimal TradeAmount { get; set; }
        public decimal Fee { get; set; }
        public string Delivery { get; set; }
        public string Remark { get; set; }
    }
}