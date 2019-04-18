using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Production.UI.CultureFields.Models
{
    [Table("culture_field")]
    public class CultureField
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("code_name")]
        public string codeName { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("data_type")]
        public string DataType { set; get; }

        [Column("source")]
        public string Source { set; get; }

        [Column("minimum")]
        public string Minimum { set; get; }

        [Column("maximum")]
        public string Maximum { set; get; }

        [Column("is required")]
        public bool IsRequired { set; get; }


        [Column("is_used")]
        public bool IsUsed { set; get; }

        [JsonIgnore]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

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
