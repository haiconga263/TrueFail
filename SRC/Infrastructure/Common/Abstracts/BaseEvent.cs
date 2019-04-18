using Common.Models;
using System.Collections.Generic;
using System.Data;

namespace Common.Abstracts
{
    public abstract class BaseEvent<T>
    {
        public virtual EventResultModel Before(object sender, IEnumerable<T> value, IDbConnection connection, IDbTransaction transaction, UserSession user)
        {
            return new EventResultModel();

        }
        public virtual EventResultModel After(object sender, IEnumerable<T> value, IDbConnection connection, IDbTransaction transaction, UserSession user)
        {
            return new EventResultModel();
        }
        public virtual EventResultModel BeforeRecord(object sender, T value, IDbConnection connection, IDbTransaction transaction, UserSession user)
        {
            return new EventResultModel();
        }
        public virtual EventResultModel AfterRecord(object sender, T value, IDbConnection connection, IDbTransaction transaction, UserSession user)
        {
            return new EventResultModel();
        }
    }
}
