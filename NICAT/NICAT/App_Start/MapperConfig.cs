using AutoMapper;
using NICAT.Models;
using NICAT.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT
{
    public class ProfileGlobal : Profile
    {
        public void DoMap()
        {
            CreateMap<DeliveryType, DeliveryTypeDTOs>();
            CreateMap<Nation, NationDTOs>();
            CreateMap<Supplier, SupplierDTOs>();
            CreateMap<Commodity, CommodityDTOs>();
            CreateMap<Order, OrderDTOs>();
            CreateMap<Receipt, ReceiptDTOs>();
            CreateMap<Purchase, PurchaseDTOs>();
            CreateMap<Trading, TradingDTOs>();
        }
    }

    public static class MapperConfig
    {
        private static MapperConfiguration MapperCfg;
        public static IMapper mapper { private set; get; }

        static public void Do()
        {
            MapperCfg = MapperCfg ?? new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfileGlobal>();
            });

            mapper = mapper ?? MapperCfg.CreateMapper();
        }
    }
}