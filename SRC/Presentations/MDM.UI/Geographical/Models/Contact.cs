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
    [Table("contact")]
    public class Contact : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [JsonIgnore]
        [Column("object_type")]
        public string ObjectType { set; get; }

        [Column("object_id")]
        public int ObjectId { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("phone")]
        public string Phone { set; get; }

        [Column("email")]
        public string Email { set; get; }

        [Column("gender")]
        public string Gender { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
