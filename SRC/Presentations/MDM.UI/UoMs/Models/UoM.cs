using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.UoMs.Models
{
    [Table("uom")]
    public class UoM : BaseModel
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Decription { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
