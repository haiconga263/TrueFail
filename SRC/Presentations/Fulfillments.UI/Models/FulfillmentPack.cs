using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fulfillments.UI.Models
{
    [Table("fulfillment_fr_order")]
    public class FulfillmentPack : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }
        [Column("code")]
        public string Code { set; get; }
        [Column("team_id")]
        public int TeamId { set; get; }
        [Column("status")]
        public int Status { set; get; }
    }
}
