using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common.Interfaces
{
    public interface IBaseRepository
    {
        
        void JoinTransaction(IDbConnection conn, IDbTransaction trans);
    }
}
