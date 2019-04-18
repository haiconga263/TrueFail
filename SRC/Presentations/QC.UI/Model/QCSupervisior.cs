using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QC.UI.Model
{
    [Table("qc_supervisior") ]
    public class QCSupervisior :  BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [ForeignKey("")]
        [Column("staff_id")]
        public int  StaffID { get; set; }

        [Column("farm_id")]
        public int FarmID { get; set; }

    }
}
