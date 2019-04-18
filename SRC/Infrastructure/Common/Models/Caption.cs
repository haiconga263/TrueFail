using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    [Table("caption")]
    public class Caption
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("type")]
        public short Type { set; get; }

        [Column("default_caption")]
        public string DefaultCaption { set; get; }
    }
}
