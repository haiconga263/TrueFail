using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.ProductLanguages.Models
{
    [Table("product_language")]
    public class ProductLanguage
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("language_id")]
        public int LanguageId { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }
    }
}
