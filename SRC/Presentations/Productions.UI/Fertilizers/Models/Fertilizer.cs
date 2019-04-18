using Common.Attributes;
using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Productions.UI.Fertilizers.Models
{
    [Table(Fertilizer.TABLENAME)]
    public class Fertilizer : BaseModel
    {
        public const string TABLENAME = "farm_fertilizer";

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
