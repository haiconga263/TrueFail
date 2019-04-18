using MDM.UI.CaptionLanguages.Models;
using MDM.UI.Captions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Captions.ViewModels
{
    public class CaptionMultipleLanguage : Caption
    {
        public List<CaptionLanguage> Languages { get; set; }
    }
}
