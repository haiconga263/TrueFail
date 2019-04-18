using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fulfillments.UI.Models
{
    [Table("fulfillment_collection")]
    public class FulfillmentCollection : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int ID { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("collection_id")]
        public int CollectionId { get; set; }
        [Column("fulfillment_id")]
        public int FulfillmentId { get; set; }
        [Column("delivery_date")]
        public DateTime DeliveryDate { get; set; }
        [Column("status_id")]
        public int StatusId { get; set; }
    }
}
