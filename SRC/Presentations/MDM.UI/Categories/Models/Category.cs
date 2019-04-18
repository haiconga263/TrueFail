using Common.Attributes;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Categories.Models
{
    [Table("category")]
    public class Category : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("default_name")]
        public string Name { set; get; }

        [Column("parent_id")]
        public int? ParentId { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

    }
}
