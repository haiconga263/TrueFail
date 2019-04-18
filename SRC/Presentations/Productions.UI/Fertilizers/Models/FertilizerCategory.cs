using Common.Attributes;
using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Productions.UI.Fertilizers.Models
{

    [Table(FertilizerCategory.TABLENAME)]
    public class FertilizerCategory : BaseModel
    {
        public const string TABLENAME = "farm_fertilizer_category";

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
