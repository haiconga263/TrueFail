using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Wards.Models
{
    [Table("ward")]
    public class Ward
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("country_id")]
        public int? CountryId { set; get; }

        [Column("district_id")]
        public int? DistrictId { set; get; }

        [Column("province_id")]
        public int? ProvinceId { set; get; }

        [JsonIgnore]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

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
