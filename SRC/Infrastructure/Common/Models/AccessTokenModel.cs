using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class AccessTokenModel
    {
        public string AccessToken { set; get; }
        public DateTime LoginDate { set; get; }
        public DateTime ExpiredDate { set; get; }
    }
}
