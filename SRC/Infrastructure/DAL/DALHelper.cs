extern alias MySqlData;

using Common;
using Common.Attributes;
using Common.Helpers;
using Common.Implementations;
using DAL.Implementations;
using DAL.Interfaces;
using DAL.Models;
using Dapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DAL
{
    public class DALHelper
    {
        public static string ConnectionString => GlobalConfiguration.SQLConnectionString;
        public static string NoConnectionString => GlobalConfiguration.NoSQLConnectionString;
        public static string NoSqlDatabase => GlobalConfiguration.NoSQLDatabase;
        public static IDbConnection GetConnection(string connectionString = "", SQLType type = SQLType.MySql)
        {
            if("".Equals(connectionString))
            {
                connectionString = ConnectionString;
            }

            switch (type)
            {
                case SQLType.SQLServer:
                    return new SqlConnection(connectionString);
                case SQLType.MySql:
                    return new MySqlData.MySql.Data.MySqlClient.MySqlConnection(connectionString);
                default:
                    throw new Exception("Wrong type");
            }
        }
        public static INoSQLDbConnection GetNoSQLConnection(string connectionString = "", string database = "", NoSQLType type = NoSQLType.MongoDB)
        {
            if ("".Equals(NoConnectionString))
            {
                connectionString = NoConnectionString;
            }

            if ("".Equals(database))
            {
                database = NoSqlDatabase;
            }

            switch (type)
            {
                case NoSQLType.MongoDB:
                    return new MongoSqlConnection(connectionString, database);
                default:
                    throw new Exception("Wrong type");
            }
        }
        public static async Task<IEnumerable<T>> Query<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SQLType type = SQLType.MySql)
        {
            if (connection != null)
            {
                if(connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, dbTransaction);
            }
            else
            {
                switch(type)
                {
                    case SQLType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param);
                        }
                    case SQLType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param);
                        }
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }
        public static async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SQLType type = SQLType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SQLType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    case SQLType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }

        public static async Task<T> ExecuteScadar<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SQLType type = SQLType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SQLType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    case SQLType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }

        public static async Task<int> Execute(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SQLType type = SQLType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteAsync(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SQLType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);
                        }
                    case SQLType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);
                        }
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }
        public static async Task<IDictionary<string, object>> ReturnExecute(string sql, string[] outParamsName, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SQLType type = SQLType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                await connection.ExecuteAsync(sql, param, commandType: CommandType.Text, transaction: dbTransaction);

                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var item in outParamsName)
                {
                    result.Add(item, param.Get<object>(item));
                }

                return result;
            }
            else
            {
                switch (type)
                {
                    case SQLType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);

                            Dictionary<string, object> result = new Dictionary<string, object>();
                            foreach (var item in outParamsName)
                            {
                                result.Add(item, param.Get<object>(item));
                            }

                            return result;
                        }
                    case SQLType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);

                            Dictionary<string, object> result = new Dictionary<string, object>();
                            foreach (var item in outParamsName)
                            {
                                result.Add(item, param.Get<object>(item));
                            }

                            return result;
                        }
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }

        public static async Task<IEnumerable<T>> Query<T>(string collectionName, Func<T, bool> query, NoSQLType type = NoSQLType.MongoDB, IClientSessionHandle clientSession = null, INoSQLDbConnection connection = null)
        {
            if(connection != null)
            {
                var collection = connection.Database.GetCollection<T>(collectionName);
                return await ((IMongoQueryable<T>)(collection.AsQueryable().Where(query))).ToListAsync();
            }
            else
            {
                switch (type)
                {
                    case NoSQLType.MongoDB:
                        using (INoSQLDbConnection conn = new MongoSqlConnection(NoConnectionString, NoSqlDatabase))
                        {
                            var collection = conn.Database.GetCollection<T>(collectionName);
                            return await ((IMongoQueryable<T>)(collection.AsQueryable().Where(query))).ToListAsync();
                        }
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }
        public static async Task Insert<T>(string collectionName, T value, NoSQLType type = NoSQLType.MongoDB, IClientSessionHandle clientSession = null, INoSQLDbConnection connection = null) where T : MongDBBaseModel
        {
            if (connection != null)
            {
                var collection = connection.Database.GetCollection<T>(collectionName);
                await collection.InsertOneAsync(value);
            }
            else
            {
                switch (type)
                {
                    case NoSQLType.MongoDB:
                        using (INoSQLDbConnection conn = new MongoSqlConnection(NoConnectionString, NoSqlDatabase))
                        {
                            var collection = connection.Database.GetCollection<T>(collectionName);
                            await collection.InsertOneAsync(value);
                        }
                        break;
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }
        public static async Task Inserts<T>(string collectionName, IEnumerable<T> values, NoSQLType type = NoSQLType.MongoDB, IClientSessionHandle clientSession = null, INoSQLDbConnection connection = null) where T : MongDBBaseModel
        {
            if (connection != null)
            {
                var collection = connection.Database.GetCollection<T>(collectionName);
                await collection.InsertManyAsync(values);
            }
            else
            {
                switch (type)
                {
                    case NoSQLType.MongoDB:
                        using (INoSQLDbConnection conn = new MongoSqlConnection(NoConnectionString, NoSqlDatabase))
                        {
                            var collection = connection.Database.GetCollection<T>(collectionName);
                            await collection.InsertManyAsync(values);
                        }
                        break;
                    default:
                        throw new Exception("Wrong type");
                }
            }
        }
        #region Helper
        public static DynamicParameters ParamsWrapperWithSpInfor<T>(string spName, T obj, out string[] outputList)
        {
            var result = new DynamicParameters();
            if (CacheProvider.ICacheProviderInstance.TryGetValue(CachingConstant.SpParameterKey, out IEnumerable<SpParameter> value))
            {
                List<string> output = new List<string>();
                foreach (var param in value.Where(p => p.SpName == spName))
                {
                    if (param.IsOutput)
                    {
                        result.Add(name: param.ParamName,
                                   dbType: GetDBTypeByName(param.TypeName),
                                   direction: ParameterDirection.Output,
                                   size: param.ParamLength > 0 ? (int?)param.ParamLength : null);
                        output.Add(param.ParamName);
                    }
                    else
                    {
                        if (obj != null)
                        {
                            string properityName = param.ParamName.Replace("@", "");
                            PropertyInfo propertyInfo = typeof(T).GetProperty(properityName);
                            if (propertyInfo != null && !propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappingAttribute)))
                            {
                                if (param.IsTableType)
                                {
                                    Type tableType = null;
                                    MethodInfo methodInfo = typeof(EntityConvertHelper).GetMethod("ToDataTableWithTableColumnInfor");
                                    var assemblyNames = AppDomain.CurrentDomain.GetAssemblies();
                                    foreach (var item in assemblyNames)
                                    {
                                        tableType = item.GetTypes().Where(t => t.FullName.EndsWith(param.TypeName)).FirstOrDefault();
                                        if (tableType != null)
                                        {
                                            break;
                                        }
                                    }
                                    if (tableType != null && methodInfo != null)
                                    {
                                        if (CacheProvider.ICacheProviderInstance.TryGetValue(CachingConstant.DefinedTableType, out IEnumerable<DefinedTableType> definedTableTypes))
                                        {
                                            methodInfo = methodInfo.MakeGenericMethod(tableType);
                                            var objTable = propertyInfo.GetValue(obj);
                                            result.Add(param.ParamName,
                                                   methodInfo.Invoke(null, new object[] { objTable, definedTableTypes.Where(d => d.TableName.Equals(param.TypeName)).Select(d => d.ColumnName).ToList() }),
                                                   DbType.Object,
                                                   ParameterDirection.Input,
                                                   param.ParamLength > 0 ? (int?)param.ParamLength : null);
                                        }
                                    }
                                }
                                else
                                {
                                    result.Add(param.ParamName,
                                               propertyInfo.GetValue(obj),
                                               GetDBTypeByName(param.TypeName),
                                               ParameterDirection.Input,
                                               param.ParamLength > 0 ? (int?)param.ParamLength : null);
                                }
                            }
                        }
                    }
                }
                outputList = output.ToArray();
            }
            else
            {
                outputList = new string[0];
            }
            return result;
        }
        public static DbType GetDBTypeByName(string name)
        {
            if (name.Contains("char"))
            {
                return DbType.String;
            }
            else if (name.Equals("int"))
            {
                return DbType.Int32;
            }
            else if (name.Equals("bigint"))
            {
                return DbType.Int64;
            }
            else if (name.Equals("smallint"))
            {
                return DbType.Int16;
            }
            else if (name.Equals("uniqueidentifier"))
            {
                return DbType.Guid;
            }
            else if (name.Equals("date"))
            {
                return DbType.Date;
            }
            else if (name.Equals("datetime"))
            {
                return DbType.DateTime;
            }
            else if (name.Equals("datetime2"))
            {
                return DbType.DateTime2;
            }
            else if (name.Equals("bit"))
            {
                return DbType.Boolean;
            }
            else if (name.Equals("float"))
            {
                return DbType.Double;
            }
            else if (name.Equals("decimal"))
            {
                return DbType.Decimal;
            }
            else
            {
                return DbType.Object;
            }
        }
        #endregion
    }
}
