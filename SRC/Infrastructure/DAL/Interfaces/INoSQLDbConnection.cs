using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface INoSQLDbConnection : IDisposable
    {
        MongoClient Client { get; }
        IMongoDatabase Database { get; }
        string ConnectionString { get; set; }

        IClientSessionHandle BeginTransaction();
        Task<IClientSessionHandle> BeginTransactionAsync();
        void ChangeDatabase(string databaseName);
        void ChangeClient(string connectionStr);
    }
}
