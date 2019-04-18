using Common;
using Common.Models;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class IntegrationArrayBaseCommand<T> : BaseCommand<int> where T : BaseModel
    {
        public IEnumerable<T> Value { set; get; }
        public IntegrationArrayBaseCommand(IEnumerable<T> value)
        {
            Value = value;
        }
    }
}
