using MDM.UI.Enumerates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Addresses.Models
{
    [Table("address")]
    public class Address
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("object_type")]
        public string Object_type { set; get; }

        [Column("object_id")]
        public int? ObjectId { set; get; }

        [Column("street")]
        public string Street { set; get; }

        [Column("country_id")]
        public int? CountryId { set; get; }

        [Column("province_id")]
        public int? ProvinceId { set; get; }

        [Column("district_id")]
        public int? DistrictId { set; get; }

        [Column("ward_id")]
        public int? WardId { set; get; }

        [Column("longitude")]
        public string Longitude { set; get; }

        [Column("latitude")]
        public string Latitude { set; get; }

        [JsonIgnore]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

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
