using Common.Models;
using System.Collections.Generic;

namespace Common.ViewModels
{
    public class CaptionViewModel : Caption
    {
        public List<CaptionLanguage> Languages { set; get; } = new List<CaptionLanguage>();
    }
}
