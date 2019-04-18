using Common.Interfaces;
using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Collections.Interfaces
{
    public interface ICollectionEmployeeQueries : IBaseQueries
    {
        Task<IEnumerable<CollectionEmployee>> Gets(string condition = "");
        Task<IEnumerable<CollectionEmployee>> GetsByCollection(int collectionId);
    }
}
