using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.UnitOfMeasurements.Models
{
    [Table("uom")]
    public class UOM
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
