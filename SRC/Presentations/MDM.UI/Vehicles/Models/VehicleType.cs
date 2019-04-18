using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Vehicles.Models
{
    [Table("vehicle_type")]
    public class VehicleType
    {
        [Key]
        [Column("id")]
        public short Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
