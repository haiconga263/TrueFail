using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GS1.UI.ProductionImages.Models
{
    [Table("production_image")]
    public class ProductionImage
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("front")]
        public string Front { set; get; }

        [Column("left")]
        public string Left { set; get; }

        [Column("top")]
        public string Top { set; get; }

        [Column("back")]
        public string Back { set; get; }

        [Column("right")]
        public string Right { set; get; }

        [Column("bottom")]
        public string Bottom { set; get; }

    }
}
