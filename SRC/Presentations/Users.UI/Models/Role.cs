using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Users.UI.Models
{
    [Table("role")]
    public class Role
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_external_role")]
        public bool IsExternalRole { get; set; }

        [Column("is_used")]
        [JsonIgnore]
        public bool IsUsed { get; set; }
    }
}
