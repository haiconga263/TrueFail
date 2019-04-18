using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Products.Models
{
    [Table("product")]
    public class Product
    {

        //`id`, `code`, `image_path`, `default_name`, `default_decription`, `category_id`

        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("image_path")]
        public string ImagePath { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("default_name")]
        public string DefaultName { set; get; }

        [Column("default_decription")]
        public string DefaultDecription { set; get; }

        [Column("category_id")]
        public int CategoryId { set; get; }



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
