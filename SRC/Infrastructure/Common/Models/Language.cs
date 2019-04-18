using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    [Table("language")]
    public class Language
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }


        [Column("name")]
        public string Name { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("description")]
        public string Description { set; get; }
    }
}
