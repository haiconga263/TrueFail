using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common.Models
{
    public class BaseQueries
    {
        public UserSession LoginSession { set; get; }
        public IDbConnection DbConnection { set; get; }
        public IDbTransaction DbTransaction { set; get; }
        public void JoinTransaction(IDbConnection conn, IDbTransaction trans)
        {
            DbConnection = conn;
            DbTransaction = trans;
        }
    }
}
