using MDM.UI.Enumerates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Contacts.Models
{
    [Table("contact")]
    public class Contact
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("object_type")]
        public string ObjectType { set; get; }

        [Column("object_id")]
        public int? ObjectId { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("phone")]
        public string Phone { set; get; }

        [Column("email")]
        public string Email { set; get; }

        [Column("gender")]
        public string Gender { set; get; }

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
