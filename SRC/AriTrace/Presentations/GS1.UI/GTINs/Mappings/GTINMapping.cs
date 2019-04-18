using GS1.UI.GTINs.Models;
using GS1.UI.GTINs.ViewModels;
using Newtonsoft.Json;

namespace GS1.UI.GTINs.Mappings
{
    public static class GTINMapping
    {
        public static GTINInformation ToInformation(this GTIN gTIN)
        {
            var serializedParent = JsonConvert.SerializeObject(gTIN);
            var info = JsonConvert.DeserializeObject<GTINInformation>(serializedParent);

            return info;
        }
    }
}
