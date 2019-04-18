using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class BaseRepository
    {
        public IDbConnection DbConnection { set; get; }
        public IDbTransaction DbTransaction { set; get; }
        public void JoinTransaction(IDbConnection conn, IDbTransaction trans)
        {
            DbConnection = conn;
            DbTransaction = trans;
        }
    }
}
