using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Storages.Models
{
    [Table("storage")]
    public class Storage
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("key")]
        public string Key { get; set; }

        [Column("value")]
        public long Value { get; set; }

        [Column("prefix")]
        public string Prefix { get; set; }

        [Column("numeric_length")]
        public int NumericLength { get; set; }

        [Column("has_date")]
        public bool HasDate { get; set; }
    }
}
