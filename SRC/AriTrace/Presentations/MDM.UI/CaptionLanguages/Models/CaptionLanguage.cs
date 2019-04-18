using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDM.UI.CaptionLanguages.Models
{
    [Table("caption_language")]
    public class CaptionLanguage
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("language_id")]
        public int LanguageId { get; set; }


        [Column("caption_id")]
        public int CaptionId { get; set; }

        [Column("caption")]
        public string Caption { get; set; }

    }
}
