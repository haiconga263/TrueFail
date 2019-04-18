using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common.Interfaces
{
    public interface IBaseQueries
    {
        UserSession LoginSession { set; get; }
        void JoinTransaction(IDbConnection conn, IDbTransaction trans);
    }
}
