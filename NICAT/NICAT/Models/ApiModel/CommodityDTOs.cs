using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models.ApiModel
{
    public class CommodityDTOs
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string NationID { get; set; }
        public string SupplierID { get; set; }
        public string SupplierProductNo { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string Remark { get; set; }
    }
}