using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class UserSession
    {
        public long SessionId { set; get; }

        public string AccessToken { set; get; }

        [JsonIgnore]
        public int LoginResult { set; get; }

        [JsonIgnore]
        public string LoginCaptionMessage { set; get; }

        public int LanguageId { set; get; } = 1; //Default VietNamese first

        public string LanguageCode { set; get; } = "vi"; //Default VietNamese: vi

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsSuperAdmin { get; set; }

        [JsonIgnore]
        public bool IsUsed { get; set; }

        public List<string> Roles { set; get; } = new List<string>();
    }
}
