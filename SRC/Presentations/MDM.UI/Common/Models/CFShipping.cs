using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Common.Models
{
    [Table("cf_shipping")]
    public class CFShipping : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("collection_id")]
        public int CollectionId { set; get; }

        [Column("fulfillment_id")]
        public int? FulfillmentId { set; get; }

        [Column("shipper_id")]
        public int? ShipperId { set; get; }

        [Column("vehicle_id")]
        public int? VehicleId { set; get; }

        [Column("delivery_date")]
        public DateTime DeliveryDate { set; get; }

        [Column("status_id")]
        public short StatusId { set; get; }
    }
}
