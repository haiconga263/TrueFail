using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlRepository
    {
        public async Task<IEnumerable<DefinedTableType>> GetDefinedTableType()
        {
            string commandStr =
                @"SELECT t.name as TableName,
	                     c.name as ColumnName,
                         c.max_length as Length,
                         c.is_nullable as IsNull
                  FROM sys.columns c
                  JOIN sys.tables t ON c.[object_id] = t.object_id
                  ORDER By t.name, column_id";
            return await DALHelper.Query<DefinedTableType>(commandStr);
        }

        public async Task<IEnumerable<SpParameter>> GetSqlParameters()
        {
            string commandStr =
                @"select p.name as ParamName,
	                     p.max_length as ParamLength,
	                     p.is_output as IsOutput,
	                     pr.name as SpName,
	                     t.name as TypeName,
	                     t.is_table_type as IsTableType
                  from sys.parameters p
                       inner join sys.procedures pr on p.object_id = pr.object_id 
                       inner join sys.types t on p.system_type_id = t.system_type_id AND p.user_type_id = t.user_type_id";
            return await DALHelper.Query<SpParameter>(commandStr);
        }
    }
}
