using NICAT.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NICAT.Models
{
    public class GBService : IDisposable
    {
        private GroupBuyEntities _Entity;
        public GBService()
        {
            _Entity = new GroupBuyEntities();
        }

        #region Common

        async protected Task<string> Save<T>(EntityState action, T item)
            where T : class, new()
        {
            bool success = false;
            _Entity.Entry<T>(item).State = action;
            int saved = await _Entity.SaveChangesAsync();
            if (action == EntityState.Added || action == EntityState.Deleted)
            {
                success = saved > 0;
            }
            else if (action == EntityState.Modified)
            {
                success = saved >= 0;
            }

            string action_name = action.ToString();
            if (action == EntityState.Added)
                action_name = "新增";
            else if (action == EntityState.Modified)
                action_name = "修改";
            else if (action == EntityState.Deleted)
                action_name = "刪除";
            return action_name + (success ? "成功" : "失敗");
        }

        #endregion

        #region DeliveryType 

        /// <summary>
        /// DeliveryType Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<DeliveryType> LookupDeliveryType()
        {
            return _Entity.DeliveryType.AsNoTracking();
        }

        /// <summary>
        /// DeliveryType Get
        /// </summary>
        /// <param name="xID"></param>
        /// <returns></returns>
        public DeliveryType GetDeliveryType(string xID)
        {
            return LookupDeliveryType().SingleOrDefault(w => w.ID == xID);
        }

        /// <summary>
        /// DeliveryType Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddDeliveryType(DeliveryType item)
        {
            return await Save<DeliveryType>(EntityState.Added, item);
        }

        /// <summary>
        /// DeliveryType Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateDeliveryType(DeliveryType item)
        {
            return await Save<DeliveryType>(EntityState.Modified, item);
        }

        /// <summary>
        /// DeliveryType Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteDeliveryType(DeliveryType item)
        {
            return await Save<DeliveryType>(EntityState.Deleted, item);
        }

        #endregion

        #region Nation 

        /// <summary>
        /// Nation Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<Nation> LookupNation()
        {
            return _Entity.Nation.AsNoTracking();
        }

        /// <summary>
        /// Nation Get
        /// </summary>
        /// <param name="xID"></param>
        /// <returns></returns>
        public Nation GetNation(string xID)
        {
            return LookupNation().SingleOrDefault(w => w.ID == xID);
        }

        /// <summary>
        /// Nation Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddNation(Nation item)
        {
            return await Save<Nation>(EntityState.Added, item);
        }

        /// <summary>
        /// Nation Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateNation(Nation item)
        {
            return await Save<Nation>(EntityState.Modified, item);
        }

        /// <summary>
        /// Nation Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteNation(Nation item)
        {
            return await Save<Nation>(EntityState.Deleted, item);
        }

        #endregion

        #region Supplier 

        /// <summary>
        /// Supplier Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<Supplier> LookupSupplier()
        {
            return _Entity.Supplier.AsNoTracking();
        }

        /// <summary>
        /// Supplier Get
        /// </summary>
        /// <param name="xID"></param>
        /// <returns></returns>
        public Supplier GetSupplier(string xID)
        {
            return LookupSupplier().SingleOrDefault(w => w.ID == xID);
        }

        /// <summary>
        /// Supplier Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddSupplier(Supplier item)
        {
            return await Save<Supplier>(EntityState.Added, item);
        }

        /// <summary>
        /// Supplier Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateSupplier(Supplier item)
        {
            return await Save<Supplier>(EntityState.Modified, item);
        }

        /// <summary>
        /// Supplier Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteSupplier(Supplier item)
        {
            return await Save<Supplier>(EntityState.Deleted, item);
        }

        #endregion

        #region Commodity 

        /// <summary>
        /// Commodity Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<Commodity> LookupCommodity()
        {
            return _Entity.Commodity.AsNoTracking();
        }

        /// <summary>
        /// Commodity Get
        /// </summary>
        /// <param name="xID"></param>
        /// <returns></returns>
        public Commodity GetCommodity(string xID)
        {
            return LookupCommodity().SingleOrDefault(w => w.ID == xID);
        }

        /// <summary>
        /// Commodity Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddCommodity(Commodity item)
        {
            item.ID = GetNewID_Commodity();
            return await Save<Commodity>(EntityState.Added, item);
        }

        /// <summary>
        /// Commodity Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateCommodity(Commodity item)
        {
            return await Save<Commodity>(EntityState.Modified, item);
        }

        /// <summary>
        /// Commodity Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteCommodity(Commodity item)
        {
            return await Save<Commodity>(EntityState.Deleted, item);
        }

        private string GetNewID_Commodity()
        {
            ///C20171007001
            string xNewID = string.Format("C{0}", DateTime.Today.ToString("yyyyMMdd"));
            int xNewNo = 1;
            var xData = _Entity.Commodity.Where(w => w.ID.StartsWith(xNewID)).OrderByDescending(o => o.ID).FirstOrDefault();
            if (xData != null)
                xNewNo = int.Parse(xData.ID.Substring(xData.ID.Length - 3)) + 1;

            return string.Concat(xNewID, xNewNo.ToString("000"));
        }

        #endregion

        #region Order 

        /// <summary>
        /// Order Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<Order> LookupOrder(OrderQueryViewModel filter = null)
        {
            var q = _Entity.Order.AsNoTracking().AsQueryable();
            if (filter.IsNotNull())
            {
                if (filter.TradeDate_S.HasValue)
                    q = q.Where(w => w.TradeDate >= filter.TradeDate_S.Value);
                if (filter.TradeDate_E.HasValue)
                    q = q.Where(w => w.TradeDate <= filter.TradeDate_E.Value);
                if (filter.CommodityID.IsNotNullOrEmpty())
                    q = q.Where(w => w.CommodityID == filter.CommodityID);
                if (filter.TransNos.IsNotEmpty())
                    q = q.Where(w => filter.TransNos.Contains(w.TransNo));
                if (filter.IsPaid.HasValue)
                    q = q.Where(w => (string.IsNullOrEmpty(w.ReceiptNo) == filter.IsPaid.Value) == false);
                if (filter.IsPurchased.HasValue)
                    q = q.Where(w => (string.IsNullOrEmpty(w.PurchaseNo) == filter.IsPurchased.Value) == false);
                if (filter.ReceiptNo.IsNotNullOrEmpty())
                    q = q.Where(w => w.ReceiptNo == filter.ReceiptNo);
                if (filter.PurchaseNo.IsNotNullOrEmpty())
                    q = q.Where(w => w.PurchaseNo == filter.PurchaseNo);
            }

            return q;
        }

        /// <summary>
        /// Order Get
        /// </summary>
        /// <param name="xTransNo"></param>
        /// <returns></returns>
        public Order GetOrder(string xTransNo)
        {
            return LookupOrder().SingleOrDefault(w => w.TransNo == xTransNo);
        }

        /// <summary>
        /// Order Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddOrder(Order item)
        {
            item.TransNo = GetNewTransNo_Order(item);
            return await Save<Order>(EntityState.Added, item);
        }

        /// <summary>
        /// Order Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateOrder(Order item)
        {
            return await Save<Order>(EntityState.Modified, item);
        }

        /// <summary>
        /// Order Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteOrder(Order item)
        {
            return await Save<Order>(EntityState.Deleted, item);
        }

        private string GetNewTransNo_Order(Order item)
        {
            ///O20171007001
            string xNewTransNo = string.Format("O{0}", item.TradeDate.ToString("yyyyMMdd"));
            int xNewNo = 1;
            var xData = LookupOrder(new OrderQueryViewModel()
            {
                TradeDate_S = item.TradeDate,
                TradeDate_E = item.TradeDate
            })
            .Where(w => w.TransNo.StartsWith(xNewTransNo))
            .OrderByDescending(o => o.TransNo)
            .FirstOrDefault();
            if (xData != null)
                xNewNo = int.Parse(xData.TransNo.Substring(xData.TransNo.Length - 3)) + 1;

            return string.Concat(xNewTransNo, xNewNo.ToString("000"));
        }

        #endregion

        #region Trading 

        /// <summary>
        /// Trading Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<Trading> LookupTrading(TradingQueryViewModel filter = null)
        {
            var q = _Entity.Trading.AsNoTracking().AsQueryable();
            if (filter.IsNotNull())
            {
                if (filter.TradeDate_S.HasValue)
                    q = q.Where(w => w.TradeDate >= filter.TradeDate_S.Value);
                if (filter.TradeDate_E.HasValue)
                    q = q.Where(w => w.TradeDate <= filter.TradeDate_E.Value);
                if (filter.Buyer.IsNotNullOrEmpty())
                    q = q.Where(w => w.Buyer == filter.Buyer);
                if (filter.CommodityID.IsNotNullOrEmpty())
                    q = q.Where(w => w.CommodityID == filter.CommodityID);
                if (filter.TransNos.IsNotEmpty())
                    q = q.Where(w => filter.TransNos.Contains(w.TransNo));
                if (filter.IsShipped.HasValue)
                    q = q.Where(w => (string.IsNullOrEmpty(w.ShipperNo) == filter.IsShipped.Value) == false);
                if (filter.ShipperNo.IsNotNullOrEmpty())
                    q = q.Where(w => w.ShipperNo == filter.ShipperNo);
            }

            return q;
        }

        /// <summary>
        /// Trading Get
        /// </summary>
        /// <param name="xTransNo"></param>
        /// <returns></returns>
        public Trading GetTrading(string xTransNo)
        {
            return LookupTrading().SingleOrDefault(w => w.TransNo == xTransNo);
        }

        /// <summary>
        /// Trading Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddTrading(Trading item)
        {
            item.TransNo = GetNewTransNo_Trading(item);
            return await Save<Trading>(EntityState.Added, item);
        }

        /// <summary>
        /// Trading Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateTrading(Trading item)
        {
            return await Save<Trading>(EntityState.Modified, item);
        }

        /// <summary>
        /// Trading Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteTrading(Trading item)
        {
            return await Save<Trading>(EntityState.Deleted, item);
        }

        private string GetNewTransNo_Trading(Trading item)
        {
            ///T20171007001
            string xNewTransNo = string.Format("T{0}", item.TradeDate.ToString("yyyyMMdd"));
            int xNewNo = 1;
            var xData = LookupTrading(new TradingQueryViewModel()
            {
                TradeDate_S = item.TradeDate,
                TradeDate_E = item.TradeDate
            })
            .Where(w => w.TransNo.StartsWith(xNewTransNo))
            .OrderByDescending(o => o.TransNo)
            .FirstOrDefault();
            if (xData != null)
                xNewNo = int.Parse(xData.TransNo.Substring(xData.TransNo.Length - 3)) + 1;

            return string.Concat(xNewTransNo, xNewNo.ToString("000"));
        }

        #endregion

        #region Receipt

        public IQueryable<Receipt> LookupReceipt(ReceiptQueryViewModel filter = null)
        {
            var q = _Entity.Receipt.AsNoTracking().AsQueryable();
            if (filter.IsNotNull())
            {
                if (filter.TradeDate_S.HasValue)
                    q = q.Where(w => w.TradeDate >= filter.TradeDate_S.Value);
                if (filter.TradeDate_E.HasValue)
                    q = q.Where(w => w.TradeDate <= filter.TradeDate_E.Value);
            }

            return q;
        }

        public Receipt GetReceipt(string xTransNo)
        {
            return LookupReceipt().SingleOrDefault(w => w.TransNo == xTransNo);
        }

        /// <summary>
        /// Receipt Add
        /// </summary>
        /// <param name="item"></param>
        /// <param name="xPreData"></param>
        /// <returns></returns>
        async public Task<string> AddReceipt(Receipt item, IEnumerable<Order> xPreData)
        {
            item.TransNo = GetNewTransNo_Receipt(item);
            _Entity.Entry(item).State = EntityState.Added;

            if (xPreData.IsEmpty())
                return "請選擇欲付款之下單交易";
            foreach (var it in xPreData)
            {
                it.ReceiptNo = item.TransNo;
                _Entity.Entry(it).State = EntityState.Modified;
            }

            bool success = await _Entity.SaveChangesAsync() > 0;
            return (success ? "新增成功" : "新增失敗");
        }

        /// <summary>
        /// Receipt Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateReceipt(Receipt item)
        {
            return await Save<Receipt>(EntityState.Modified, item);
        }

        /// <summary>
        /// Receipt Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteReceipt(Receipt item)
        {
            _Entity.Entry(item).State = EntityState.Deleted;
            foreach (var it in LookupOrder(new OrderQueryViewModel()
            {
                ReceiptNo = item.TransNo,
                IsPaid = true
            }))
            {
                it.ReceiptNo = string.Empty;
                _Entity.Entry(it).State = EntityState.Modified;
            }

            bool success = await _Entity.SaveChangesAsync() > 0;
            return (success ? "刪除成功" : "刪除失敗");
        }

        private string GetNewTransNo_Receipt(Receipt item)
        {
            ///R20171007001
            string xNewTransNo = string.Format("R{0}", item.TradeDate.ToString("yyyyMMdd"));
            int xNewNo = 1;
            var xData = LookupReceipt(new ReceiptQueryViewModel()
            {
                TradeDate_S = item.TradeDate,
                TradeDate_E = item.TradeDate
            })
            .Where(w => w.TransNo.StartsWith(xNewTransNo))
            .OrderByDescending(o => o.TransNo)
            .FirstOrDefault();
            if (xData != null)
                xNewNo = int.Parse(xData.TransNo.Substring(xData.TransNo.Length - 3)) + 1;

            return string.Concat(xNewTransNo, xNewNo.ToString("000"));
        }

        #endregion

        #region Purchase

        public IQueryable<Purchase> LookupPurchase(PurchaseQueryViewModel filter = null)
        {
            var q = _Entity.Purchase.AsNoTracking().AsQueryable();
            if (filter.IsNotNull())
            {
                if (filter.TradeDate_S.HasValue)
                    q = q.Where(w => w.TradeDate >= filter.TradeDate_S.Value);
                if (filter.TradeDate_E.HasValue)
                    q = q.Where(w => w.TradeDate <= filter.TradeDate_E.Value);
            }

            return q;
        }

        public Purchase GetPurchase(string xTransNo)
        {
            return LookupPurchase().SingleOrDefault(w => w.TransNo == xTransNo);
        }

        /// <summary>
        /// Purchase Add
        /// </summary>
        /// <param name="item"></param>
        /// <param name="xPreData"></param>
        /// <returns></returns>
        async public Task<string> AddPurchase(Purchase item, IEnumerable<Order> xPreData)
        {
            item.TransNo = GetNewTransNo_Purchase(item);
            _Entity.Entry(item).State = EntityState.Added;

            if (xPreData.IsEmpty())
                return "請選擇已進貨之下單交易";
            foreach (var it in xPreData)
            {
                it.ReceiptNo = item.TransNo;
                _Entity.Entry(it).State = EntityState.Modified;
            }

            bool success = await _Entity.SaveChangesAsync() > 0;
            return (success ? "新增成功" : "新增失敗");
        }

        /// <summary>
        /// Purchase Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdatePurchase(Purchase item)
        {
            return await Save<Purchase>(EntityState.Modified, item);
        }

        /// <summary>
        /// Purchase Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeletePurchase(Purchase item)
        {
            _Entity.Entry(item).State = EntityState.Deleted;
            foreach (var it in LookupOrder(new OrderQueryViewModel()
            {
                ReceiptNo = item.TransNo,
                IsPaid = true,
                IsPurchased = true
            }))
            {
                it.ReceiptNo = string.Empty;
                _Entity.Entry(it).State = EntityState.Modified;
            }

            bool success = await _Entity.SaveChangesAsync() > 0;
            return (success ? "刪除成功" : "刪除失敗");
        }

        private string GetNewTransNo_Purchase(Purchase item)
        {
            ///P20171007001
            string xNewTransNo = string.Format("P{0}", item.TradeDate.ToString("yyyyMMdd"));
            int xNewNo = 1;
            var xData = LookupPurchase(new PurchaseQueryViewModel()
            {
                TradeDate_S = item.TradeDate,
                TradeDate_E = item.TradeDate
            })
            .Where(w => w.TransNo.StartsWith(xNewTransNo))
            .OrderByDescending(o => o.TransNo)
            .FirstOrDefault();
            if (xData != null)
                xNewNo = int.Parse(xData.TransNo.Substring(xData.TransNo.Length - 3)) + 1;

            return string.Concat(xNewTransNo, xNewNo.ToString("000"));
        }

        #endregion

        #region Shipper

        public IQueryable<Shipper> LookupShipper(ShipperQueryViewModel filter = null)
        {
            var q = _Entity.Shipper.AsNoTracking().AsQueryable();
            if (filter.IsNotNull())
            {
                if (filter.TradeDate_S.HasValue)
                    q = q.Where(w => w.TradeDate >= filter.TradeDate_S.Value);
                if (filter.TradeDate_E.HasValue)
                    q = q.Where(w => w.TradeDate <= filter.TradeDate_E.Value);
                if (filter.Buyer.IsNotNullOrEmpty())
                    q = q.Where(w => w.Buyer == filter.Buyer);
            }

            return q;
        }

        public Shipper GetShipper(string xTransNo)
        {
            return LookupShipper().SingleOrDefault(w => w.TransNo == xTransNo);
        }

        /// <summary>
        /// Shipper Add
        /// </summary>
        /// <param name="item"></param>
        /// <param name="xPreData"></param>
        /// <returns></returns>
        async public Task<string> AddShipper(Shipper item, IEnumerable<Trading> xPreData)
        {
            item.TransNo = GetNewTransNo_Shipper(item);
            _Entity.Entry(item).State = EntityState.Added;

            if (xPreData.IsEmpty())
                return "請確認欲出貨之交易";
            foreach (var it in xPreData)
            {
                it.ShipperNo = item.TransNo;
                _Entity.Entry(it).State = EntityState.Modified;
            }

            bool success = await _Entity.SaveChangesAsync() > 0;
            return (success ? "新增成功" : "新增失敗");
        }

        /// <summary>
        /// Shipper Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateShipper(Shipper item)
        {
            return await Save<Shipper>(EntityState.Modified, item);
        }

        /// <summary>
        /// Shipper Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteShipper(Shipper item)
        {
            _Entity.Entry(item).State = EntityState.Deleted;
            foreach (var it in LookupTrading(new TradingQueryViewModel()
            {
                ShipperNo = item.TransNo,
                IsShipped = true
            }))
            {
                it.ShipperNo = string.Empty;
                _Entity.Entry(it).State = EntityState.Modified;
            }

            bool success = await _Entity.SaveChangesAsync() > 0;
            return (success ? "刪除成功" : "刪除失敗");
        }

        private string GetNewTransNo_Shipper(Shipper item)
        {
            ///S20171007001
            string xNewTransNo = string.Format("S{0}", item.TradeDate.ToString("yyyyMMdd"));
            int xNewNo = 1;
            var xData = LookupShipper(new ShipperQueryViewModel()
            {
                TradeDate_S = item.TradeDate,
                TradeDate_E = item.TradeDate
            })
            .Where(w => w.TransNo.StartsWith(xNewTransNo))
            .OrderByDescending(o => o.TransNo)
            .FirstOrDefault();
            if (xData != null)
                xNewNo = int.Parse(xData.TransNo.Substring(xData.TransNo.Length - 3)) + 1;

            return string.Concat(xNewTransNo, xNewNo.ToString("000"));
        }

        #endregion

        #region CashFlow

        /// <summary>
        /// CashFlow Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<CashFlow> LookupCashFlow()
        {
            return _Entity.CashFlow.AsNoTracking();
        }

        /// <summary>
        /// CashFlow Get
        /// </summary>
        /// <param name="xQueryDate"></param>
        /// <returns></returns>
        public CashFlow GetCashFlow(DateTime xQueryDate)
        {
            return LookupCashFlow().SingleOrDefault(w => w.TradeDate == xQueryDate);
        }

        /// <summary>
        /// CashFlow Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddCashFlow(CashFlow item)
        {
            return await Save<CashFlow>(EntityState.Added, item);
        }

        /// <summary>
        /// CashFlow Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateCashFlow(CashFlow item)
        {
            return await Save<CashFlow>(EntityState.Modified, item);
        }

        /// <summary>
        /// CashFlow Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteCashFlow(CashFlow item)
        {
            return await Save<CashFlow>(EntityState.Deleted, item);
        }

        #endregion

        #region CommodityMast

        /// <summary>
        /// CommodityMast Lookup
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommodityMast> LookupCommodityMast()
        {
            return _Entity.CommodityMast.AsNoTracking();
        }

        /// <summary>
        /// CommodityMast Get
        /// </summary>
        /// <param name="xQueryDate"></param>
        /// <returns></returns>
        public CommodityMast GetCommodityMast(DateTime xQueryDate, string xCommodityID)
        {
            return LookupCommodityMast().SingleOrDefault(w => w.TradeDate == xQueryDate && w.CommodityID == xCommodityID);
        }

        /// <summary>
        /// CommodityMast Add
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> AddCommodityMast(CommodityMast item)
        {
            return await Save<CommodityMast>(EntityState.Added, item);
        }

        /// <summary>
        /// CommodityMast Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> UpdateCommodityMast(CommodityMast item)
        {
            return await Save<CommodityMast>(EntityState.Modified, item);
        }

        /// <summary>
        /// CommodityMast Delete
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async public Task<string> DeleteCommodityMast(CommodityMast item)
        {
            return await Save<CommodityMast>(EntityState.Deleted, item);
        }

        #endregion

        #region Account / UnAccount

        private DateTime _BeginDate = DateTime.Parse(ConfigurationManager.AppSettings["BeginDate"]);

        async public Task<HandleMessage> DoAccound(DateTime xExeDate)
        {
            if (_BeginDate >= xExeDate)
                return HandleMessage.CreateFail(string.Format("結帳失敗, 執行日期必須大於 {0}", _BeginDate.ToString("yyyy/MM/dd")));

            DateTime? xStart = null;
            var xMast = LookupCashFlow();
            if (xMast.IsNotEmpty())
                xStart = xMast.Max(m => m.TradeDate).AddDays(1);
            else
                return HandleMessage.CreateFail("結帳失敗, 無現金流資料");

            if (xStart.HasValue.Not())
                return HandleMessage.CreateFail("結帳失敗, 無起始日期");

            if (xExeDate < xStart)
                return HandleMessage.CreateFail(string.Format("結帳失敗, 執行日期必須大於{0}", xStart.Value.ToString("yyyy/MM/dd")));

            foreach (var d in xStart.Value.ToDates(xExeDate))
            {
                var xShipper = _Entity.Shipper.AsNoTracking().Where(w => w.TradeDate == d).ToList();     ///出貨交易
                var xTrading = _Entity.Trading.AsNoTracking()                                   ///賣出交易
                    .Where(w => xShipper.Where(x => x.TransNo != string.Empty).Select(s => s.TransNo).Contains(w.ShipperNo)).ToList();
                var xReceipt = _Entity.Receipt.AsNoTracking().Where(w => w.TradeDate == d).ToList();     ///付款交易
                var xPurchase = _Entity.Purchase.AsNoTracking().Where(w => w.TradeDate == d).ToList();   ///進貨交易
                var xOrder = _Entity.Order.AsNoTracking()                                       ///下單交易
                    .Where(w => xPurchase.Where(x => x.TransNo != string.Empty).Select(s => s.TransNo).Contains(w.PurchaseNo)).ToList();

                var xYDAY = GetCashFlow(d.AddDays(-1));
                var item = new CashFlow();
                item.TradeDate = d;
                item.Income = xShipper.Sum(s => s.TradeAmount);
                item.Expenses = xShipper.Sum(s => s.Fee) + xReceipt.Sum(s => s.TradeAmount) + xPurchase.Sum(s => s.Fee);
                item.Balance = (xYDAY.IsNotNull() ? xYDAY.Balance : decimal.Zero) + item.Income - item.Expenses;
                _Entity.Entry(item).State = EntityState.Added;

                foreach (var c in _Entity.Commodity.AsNoTracking().Select(s => s.ID))
                {
                    var xYDAY_CM = GetCommodityMast(d.AddDays(-1), c);
                    var item_CM = new CommodityMast();
                    item_CM.TradeDate = d;
                    item_CM.CommodityID = c;
                    item_CM.Quantity = (xYDAY_CM.IsNotNull() ? xYDAY_CM.Quantity : decimal.Zero) -
                        xTrading.Where(w => w.CommodityID == c).Sum(s => s.TradeQuantity) +
                        xOrder.Where(w => w.CommodityID == c).Sum(s => s.TradeQuantity);
                    _Entity.Entry(item).State = EntityState.Added;
                }
            }

            bool success = await _Entity.SaveChangesAsync() > 0;
            return HandleMessage.Create(success, "結帳成功", "結帳失敗");
        }

        async public Task<HandleMessage> DoUnAccound(DateTime xExeDate)
        {
            if (_BeginDate >= xExeDate)
                return HandleMessage.CreateFail(string.Format("回帳失敗, 執行日期必須大於 {0}", _BeginDate.ToString("yyyy/MM/dd")));

            var xCashFlow = LookupCashFlow().Where(w => w.TradeDate >= xExeDate).ToList();
            var xCommodityMast = LookupCommodityMast().Where(w => w.TradeDate >= xExeDate).ToList();

            if (xCashFlow.IsEmpty() && xCommodityMast.IsEmpty())
                return HandleMessage.CreateFail("回帳失敗, 無現金流資料");

            foreach (var item in xCashFlow)
                _Entity.Entry(item).State = EntityState.Deleted;
            foreach (var item in xCommodityMast)
                _Entity.Entry(item).State = EntityState.Deleted;

            bool success = await _Entity.SaveChangesAsync() > 0;
            return HandleMessage.Create(success, "回帳成功", "回帳失敗");
        }

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
            GC.SuppressFinalize(this);
        }
    }
}