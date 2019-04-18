using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDM.UI.Captions.Models
{
    [Table("caption")]
    public class Caption
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("type")]
        public int Type { get; set; }

        [Column("default_caption")]
        public string DefaultCaption { get; set; }

        [Column("is_common")]
        public bool IsCommon { get; set; }

        [JsonIgnore]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_by")]
        public int ModifiedBy { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
