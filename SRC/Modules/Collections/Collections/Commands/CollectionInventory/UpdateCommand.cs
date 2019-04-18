using MDM.UI.Collections.Models;
using Web.Controllers;

namespace Collections.Commands.CollectionInventory
{
    public class UpdateCommand : BaseCommand<int>
    {
        public short Direction { set; get; }
        public int CollectionId { set; get; }
        public string TraceCode { set; get; }
        public int ProductId { set; get; }
        public int UoMId { set; get; }
        public long Quantity { set; get; }
        public string Reason { set; get; }
        public UpdateCommand(short direction, int collectionId, string traceCode, int productId, int uoMId, long quantity, string reason)
        {
            Direction = direction;
            CollectionId = collectionId;
            TraceCode = traceCode;
            ProductId = productId;
            UoMId = uoMId;
            Quantity = quantity;
            Reason = reason;
        }
    }
}
