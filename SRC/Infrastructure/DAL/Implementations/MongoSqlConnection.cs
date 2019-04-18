using DAL.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class MongoSqlConnection : INoSQLDbConnection
    {

        private string DatabaseName { get; set; }

        public MongoClient Client { get; private set; }
        public IMongoDatabase Database { get; private set; }
        public string ConnectionString { get; set; }

        private void Dispose(bool isDispose)
        {
            if(isDispose)
            {
                Client = null;
                Database = null;
            }
        }

        public MongoSqlConnection(string connectionStr = "", string database = "")
        {
            ConnectionString = connectionStr;
            DatabaseName = database;
            if(!"".Equals(ConnectionString))
            {
                Client = new MongoClient(ConnectionString);
            }
            if (Client != null && !"".Equals(DatabaseName))
                Database = Client.GetDatabase(DatabaseName);
        }

        ~MongoSqlConnection()
        {
            Dispose(false);
        }

        public IClientSessionHandle BeginTransaction()
        {
            return Client.StartSession();
        }
        public async Task<IClientSessionHandle> BeginTransactionAsync()
        {
            return await Client.StartSessionAsync();
        }

        public void ChangeDatabase(string databaseName)
        {
            DatabaseName = databaseName;
            if (Client != null)
                Database = Client.GetDatabase(DatabaseName);
            else
            {
                throw new Exception("The DB Client does't exist");
            }
        }

        public void ChangeClient(string connectionStr)
        {
            Client = new MongoClient(connectionStr);
            if (Client == null)
            {
                Client = new MongoClient(ConnectionString);
                throw new Exception("The DB Client does't exist");
            }
            ConnectionString = connectionStr;
            Database = null;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
