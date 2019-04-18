using Common.Attributes;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Productions.UI.Pesticides.Models
{

    [Table(PesticideCategory.TABLENAME)]
    public class PesticideCategory : BaseModel
    {
        public const string TABLENAME = "farm_pesticide_category";

        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("parent_id")]
        public int? ParentId { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

    }
}
