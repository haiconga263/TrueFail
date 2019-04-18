using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distributions.UI.Models
{
    [Table("trip_status")]
    public class TripStatus
    {
        [Key]
        [Column("id")]
        public short Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("flag_color")]
        public string FlagColor { set; get; }
    }
}
