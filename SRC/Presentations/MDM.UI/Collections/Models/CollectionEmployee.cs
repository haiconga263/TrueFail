using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Collections.Models
{
    [Table("collection_employee")]
    public class CollectionEmployee
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("collection_id")]
        public int CollectionId { set; get; }

        [Column("employee_id")]
        public int EmployeeId { set; get; }
    }
}
