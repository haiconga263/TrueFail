using Common;
using Common.Models;

namespace Web.Controllers
{
    public class IntegrationBaseCommand<T> : BaseCommand<int> where T : BaseModel
    {
        public T Value { set; get; }
        public IntergrationHandleType Type { set; get; }
        public IntegrationBaseCommand(T value)
        {
            Value = value;
        }
    }
}
