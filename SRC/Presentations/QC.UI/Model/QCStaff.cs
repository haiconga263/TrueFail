using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QC.UI.Model
{
    [Table("qc_staff")]
    public class QCStaff
    {
        [Key]
        [Identity]
        [Column("id")]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("long_location")]
        public double? LongLocation { get; set; }
        [Column("lat_location")]
        public double? LatLocation { get; set; }
        [Column("long_field")]
        public double? LongField { get; set; }
        [Column("lat_field")]
        public double? LatField { get; set; }
        [Column("is_field")]
        public bool IsField { get; set; }   

    }
}
