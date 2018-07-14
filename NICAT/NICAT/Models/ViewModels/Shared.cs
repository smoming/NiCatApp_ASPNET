using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NICAT.Models
{
    static public class Shared
    {
        static private GroupBuyEntities GetEntity()
        {
            return new GroupBuyEntities();
        }

        public static string[] BuyerList()
        {
            using (var r = GetEntity())
            {
                return r.Trading.AsNoTracking().Select(s => s.Buyer).Distinct().ToArray();
            }
        }

        public static List<SelectListItem> NationList()
        {
            using (var r = GetEntity())
            {
                return r.Nation.AsNoTracking()
                    .Select(s => new SelectListItem() { Value = s.ID, Text = s.Name })
                    .ToList();
            }
        }

        public static List<SelectListItem> SupplierList()
        {
            using (var r = GetEntity())
            {
                return r.Supplier.AsNoTracking()
                    .Select(s => new SelectListItem() { Value = s.ID, Text = s.Name })
                    .ToList();
            }
        }

        public static List<SelectListItem> CommodityList()
        {
            using (var r = GetEntity())
            {
                return r.Commodity.AsNoTracking()
                    .Select(s => new SelectListItem() { Value = s.ID, Text = s.Name + " - " + s.Style })
                    .ToList();
            }
        }

        public static List<SelectListItem> DeliveryTypeList()
        {
            using (var r = GetEntity())
            {
                return r.DeliveryType.AsNoTracking()
                    .Select(s => new SelectListItem() { Value = s.ID, Text = s.Name })
                    .ToList();
            }
        }
    }
}