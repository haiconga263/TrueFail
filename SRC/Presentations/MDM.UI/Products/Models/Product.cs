using Common.Attributes;
using Common.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDM.UI.Products.Models
{
    [Table("product")]
    public class Product : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("category_id")]
        public int? CategoryId { set; get; }

        [Column("image_url")]
        public string ImageURL { set; get; }

        [Column("default_name")]
        public string DefaultName { set; get; }

        [Column("default_description")]
        public string DefaultDescription { set; get; }

        [Column("default_uom_id")]
        public int DefaultUoMId { set; get; }

        [Column("default_buying_price")]
        public decimal DefaultBuyingPrice { set; get; }

        [Column("default_selling_price")]
        public decimal DefaultSellingPrice { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
