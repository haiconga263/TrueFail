using Common.Attributes;
using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collections.UI.PurchaseOrders.Models
{
    [Table("collection_purchase_order")]
    public class PurchaseOrder : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("code")]
        public string Code { get; set; }

        [Column("date_code")]
        public string DateCode { get; set; }
    }
}
