using GS1.UI.GTINs.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GS1.UI.GTINs.Models
{
    [Table("gtin")]
    public class GTIN
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("indicator_digit")]
        public int IndicatorDigit { get; set; }

        [Column("company_code")]
        public int CompanyCode { get; set; }

        [Column("numeric")]
        public long Numeric { get; set; }

        [Column("check_digit")]
        public int CheckDigit { get; set; }

        [Column("partner_id")]
        public int PartnerId { get; set; }

        [Column("type")]
        public GTINTypes Type { get; set; }

        [Column("used_date")]
        public DateTime UsedDate { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

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
