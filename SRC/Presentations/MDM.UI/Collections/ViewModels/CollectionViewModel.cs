using MDM.UI.Collections.Models;
using MDM.UI.Geographical.Models;

namespace MDM.UI.Collections.ViewModels
{
    public class CollectionViewModel : Collection
    {
        public Address Address { set; get; }
        public Contact Contact { set; get; }

        public string ImageData { set; get; }
    }
}
