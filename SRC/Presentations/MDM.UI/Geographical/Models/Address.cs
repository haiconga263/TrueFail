using Common.Attributes;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Geographical.Models
{
    [Table("address")]
    public class Address : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [JsonIgnore]
        [Column("object_type")]
        public string ObjectType { set; get; }

        [JsonIgnore]
        [Column("object_id")]
        public int ObjectId { set; get; }

        [Column("street")]
        public string Street { set; get; }

        [Column("country_id")]
        public int CountryId { set; get; }

        [Column("province_id")]
        public int ProvinceId { set; get; }

        [Column("district_id")]
        public int DistrictId { set; get; }

        [Column("ward_id")]
        public int WardId { set; get; }

        [Column("longitude")]
        public double Longitude { set; get; }

        [Column("latitude")]
        public double Latitude { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
