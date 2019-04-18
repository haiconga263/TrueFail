using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{
    [Table("farmer_buying_calendar")]
    public class FarmerBuyingCalendar : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("farmer_id")]
        public int FarmerId { set; get; }

        [Column("buying_date")]
        public DateTime BuyingDate { set; get; }

        [Column("is_ordered")]
        public bool IsOrdered { set; get; }

        [Column("is_expired")]
        public bool IsExpired { set; get; }

        [Column("is_adaped")]
        public bool IsAdaped { set; get; }

        [Column("adap_note")]
        public string AdapNote { set; get; }
    }
}
