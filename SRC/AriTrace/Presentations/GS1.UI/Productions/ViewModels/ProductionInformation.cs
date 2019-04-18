using GS1.UI.GTINs.ViewModels;
using GS1.UI.ProductionImages.ViewModels;
using GS1.UI.Productions.Models;

namespace GS1.UI.Productions.ViewModels
{
    public class ProductionInformation : Production
    {
        public ProductionImageData ProductionImage { get; set; }
        public GTINInformation GTIN { get; set; }
    }
}
