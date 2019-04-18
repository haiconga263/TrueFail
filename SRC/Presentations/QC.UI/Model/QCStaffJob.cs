using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QC.UI.Model
{
    [Table("qc_staff_job")]
    public class QCStaffJob
    {
        [Key]
        [Identity]
        [Column("id")]
        public int ID { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("staff_id")]
        public int StaffID { get; set; }
        [Column("farm_id")]
        public int FarmID { get; set; }
    }
}
