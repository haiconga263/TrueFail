using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Categories.Models
{
    [Table("category_language")]
    public class CategoryLanguage
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }


        [Column("category_id")]
        public int CategoryId { set; get; }


        [Column("language_id")]
        public int LanguageId { set; get; }


        [Column("name")]
        public string Name { set; get; }
    }
}
