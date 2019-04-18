using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    [Table("caption_language")]
    public class CaptionLanguage
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("caption_id")]
        public int CaptionId { set; get; }

        [Column("language_id")]
        public int LanguageId { set; get; }

        [Column("caption")]
        public string Caption { set; get; }
    }
}
