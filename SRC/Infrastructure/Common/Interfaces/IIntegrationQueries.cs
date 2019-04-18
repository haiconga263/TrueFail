using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IIntegrationQueries<T>
    {
        Task<T> Get(long id);
        Task<IEnumerable<T>> Gets(string condition = "");
    }
}
