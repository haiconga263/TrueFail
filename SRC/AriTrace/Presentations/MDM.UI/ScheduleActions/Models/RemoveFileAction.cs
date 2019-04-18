using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MDM.UI.ScheduleActions.Models
{
    public class RemoveFileAction 
    {
        [JsonProperty("path")]
        public string FilePath { get; set; }
    }
}
