
using System.Collections;

namespace Web.Controls
{
    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public object Errors { get; set; }
        public int Total { get; set; }
    }
}
