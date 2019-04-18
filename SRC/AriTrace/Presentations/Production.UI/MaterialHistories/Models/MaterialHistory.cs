using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Production.UI.MaterialHistories.Models
{
    [Table("material_history")]
    public class MaterialHistory
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("material_id")]
        public int MaterialId { set; get; }

        [Column("culture_field_id")]
        public int CultureFieldId { set; get; }

        [Column("value")]
        public string Value { set; get; }

        [Column("comment")]
        public string Comment { set; get; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("deleted_by")]
        public int? DeletedBy { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }
    }
}
