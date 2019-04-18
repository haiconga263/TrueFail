using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common.Models
{
    public class CachingCommandTransactionModel
    {
        public IDbConnection Connection { set; get; }
        public IDbTransaction Transaction { set; get; }
    }
}
