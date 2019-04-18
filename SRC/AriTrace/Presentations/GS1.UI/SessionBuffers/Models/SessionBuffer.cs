using GS1.UI.Buffers.Enumerations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS1.UI.SessionBuffers.Models
{
    [Table("session_buffer")]
    public class SessionBuffer
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("session_id")]
        public long SessionId { set; get; }

        [Column("partner_id")]
        public int PartnerId { set; get; }

        [Column("data_json")]
        public string DataJson { set; get; }

        [Column("type")]
        public SessionBufferTypes Type { set; get; }

        [Column("expired_date")]
        public DateTime ExpiredDate { set; get; }

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
