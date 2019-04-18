using Common.Interfaces;
using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Collections.Interfaces
{
    public interface ICollectionInventoryHistoryQueries : IBaseQueries
    {
        Task<IEnumerable<CollectionInventoryHistory>> Gets(string conditon = "");
    }
}
