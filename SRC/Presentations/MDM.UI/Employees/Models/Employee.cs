using Common.Attributes;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Employees.Models
{
    [Table("employee")]
    public class Employee : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("full_name")]
        public string FullName { set; get; }

        [Column("user_account_id")]
        public int? UserId { set; get; }

        [Column("report_to")]
        public int? ReportTo { set; get; }

        [Column("report_to_code")]
        public string ReportToCode { set; get; }

        [Column("email")]
        public string Email { set; get; }

        [Column("phone")]
        public string Phone { set; get; }

        [Column("birthday")]
        public DateTime? Birthday { set; get; }

        [Column("gender")]
        public string Gender { set; get; }

        [Column("job_title")]
        public string JobTitle { set; get; }

        [Column("image_url")]
        public string ImageURL { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
