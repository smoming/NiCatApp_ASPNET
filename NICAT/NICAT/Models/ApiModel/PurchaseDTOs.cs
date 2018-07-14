using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models.ApiModel
{
    public class PurchaseDTOs
    {
        public string TransNo { get; set; }
        public System.DateTime TradeDate { get; set; }
        public decimal Fee { get; set; }
        public string Remark { get; set; }
    }
}