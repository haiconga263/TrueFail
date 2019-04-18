using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Distributions.Models
{
    [Table("distribution_employee")]
    public class DistributionEmployee
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("distribution_id")]
        public int DistributionId { set; get; }

        [Column("employee_id")]
        public int EmployeeId { set; get; }
    }
}
