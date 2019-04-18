using Collections.UI.PurchaseOrders.Enumerations;
using Common.Attributes;
using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collections.UI.PurchaseOrders.Models
{
    [Table("collection_purchase_order_item")]
    public class PurchaseOrderItem : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("purchase_order_id")]
        public long PurchasingOrderId { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("status_code")]
        public PurchaseOrderStatus StatusCode { set; get; }

        [Column("price_recommend")]
        public decimal PriceRecommend { set; get; }

        [Column("price_limit")]
        public decimal PriceLimit { set; get; }

        [Column("quantity")]
        public double Quantity { set; get; }

        [Column("quantity_purchased")]
        public double QuantityPurchased { set; get; }

        [Column("uom_name")]
        public string UomName { set; get; }
    }
}
