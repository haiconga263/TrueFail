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
    [Table(Pesticide.TABLENAME)]
    public class Pesticide : BaseModel
    {
        public const string TABLENAME = "farm_pesticide";

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

        [Column("category_id")]
        public int? CategoryId { set; get; }


        [Column("is_used")]
        public bool IsUsed { get; set; }

    }
}
