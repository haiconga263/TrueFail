using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Geographical.Models
{
    [Table("province")]
    public class Province : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("phone_code")]
        public string PhoneCode { set; get; }

        [Column("country_id")]
        public int CountryId { set; get; }

        [Column("region_id")]
        public int? RegionId { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
