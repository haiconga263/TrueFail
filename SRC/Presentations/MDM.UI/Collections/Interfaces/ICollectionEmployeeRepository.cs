using Common.Interfaces;
using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Collections.Interfaces
{
    public interface ICollectionEmployeeRepository : IBaseRepository
    {
        Task<int> Add(CollectionEmployee employee);
        Task<int> Update(CollectionEmployee employee);
        Task<int> Delete(int collectionEmployeeId);
    }
}
