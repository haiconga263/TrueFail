using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Common;
using Common.Attributes;
using Common.Models;

namespace Homepage.UI.Models
{
	[Table("hp_contact")]
	public class Contact : BaseModel
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("sender_name")]
        [Required]
		public string SenderName { set; get; }

		[Column("sender_email")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string SenderEmail { set; get; }

		[Column("sender_phonenumber")]
        [Required]
        public string PhoneNumber { set; get; }

		[Column("message")]
        [Required]
        public string MessageContent { set; get; }

		[Column("status")]
		[Required]
		public Status Status { set; get; }
	}
	
}
