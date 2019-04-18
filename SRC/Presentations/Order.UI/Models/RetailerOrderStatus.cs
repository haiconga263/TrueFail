using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{
    [Table("retailer_order_status")]
    public class RetailerOrderStatus
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Key]
        [Column("caption_name")]
        public string CaptionName { set; get; }

        [Key]
        [Column("caption_description")]
        public string CaptionDescription { set; get; }
    }
}
