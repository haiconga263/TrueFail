using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Productions.UI.Seeds.Models
{
    [Table(Seed.TABLENAME)]
    public class Seed : BaseModel
    {
        public const string TABLENAME = "farm_seed";

        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
