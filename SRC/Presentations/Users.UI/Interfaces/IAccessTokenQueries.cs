using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Users.UI.Interfaces
{
    public interface IAccessTokenQueries : IBaseQueries
    {
        Task<IEnumerable<UserSession>> Gets();
        Task<UserSession> Get(string accessToken);
    }
}
