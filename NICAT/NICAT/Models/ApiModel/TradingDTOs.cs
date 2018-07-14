using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models.ApiModel
{
    public class TradingDTOs
    {
        public string TransNo { get; set; }
        public System.DateTime TradeDate { get; set; }
        public string Buyer { get; set; }
        public string CommodityID { get; set; }
        public decimal TradeQuantity { get; set; }
        public decimal TradeAmount { get; set; }
        public string ShipperNo { get; set; }
        public string Remark { get; set; }
    }
}