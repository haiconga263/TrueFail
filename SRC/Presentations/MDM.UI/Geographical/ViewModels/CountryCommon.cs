using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Geographical.ViewModels
{
    [Table("country")]
    public class CountryCommon
    {
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("phone_code")]
        public string PhoneCode { set; get; }
    }
}
