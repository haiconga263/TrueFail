using Collections.UI.POActivities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Collections.UI.POActivities.ViewModels
{
    public class POActivityInformation : POActivity
    {
        [Column("farmer_name")]
        public string FarmerName { get; set; }

        [Column("collector_name")]
        public string CollectionName { get; set; }

        public string ImageData { get; set; }
    }
}
