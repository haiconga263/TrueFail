using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Products.Models
{
    [Table("product_price")]
    public class ProductPrice
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }


        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }

        [Column("buying_price")]
        public decimal BuyingPrice { set; get; }

        [Column("selling_price")]
        public decimal SellingPrice { set; get; }

        [Column("weight")]
        public double Weight { set; get; }

        [Column("capacity")]
        public double Capacity { set; get; }

        [Column("effectived_date")]
        public DateTime EffectivedDate { set; get; }
    }
}
