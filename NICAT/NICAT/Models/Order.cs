//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NICAT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public string TransNo { get; set; }
        public System.DateTime TradeDate { get; set; }
        public string CommodityID { get; set; }
        public decimal TradeQuantity { get; set; }
        public decimal TradeAmount { get; set; }
        public string ReceiptNo { get; set; }
        public string PurchaseNo { get; set; }
        public string Remark { get; set; }
    
        public virtual Commodity Commodity { get; set; }
    }
}
