using Common.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DAL
{
    public class QueriesCreatingHelper
    {
        public static string CreateQueryInsert<T>(T obj)
        {
            var tableName = string.Empty;
            if (obj.GetType().GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is TableAttribute attributeTable)
            {
                tableName = attributeTable.Name;
            }
            else
            {
                tableName = typeof(T).Name;
            }

            var props = typeof(T).GetProperties().ToList();
            string columnNames = "";
            string values = "";
            for (int i = 0; i < props.Count; i++)
            {
                if(props[i].CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(IdentityAttribute)) != null)
                {
                    continue;
                }

                var columnName = string.Empty;
                if (props[i].GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() is ColumnAttribute attributeColumn)
                {
                    columnName = attributeColumn.Name;
                }
                else
                {
                    columnName = props[i].Name;
                }

                columnNames += $"`{columnName}`";
                values += $"{GetValue(props[i].GetValue(obj))}";
                if (i != props.Count - 1)
                {
                    columnNames += ",";
                    values += ",";
                }

            }
            string sql = $"INSERT INTO `{tableName}`({columnNames}) VALUES ({values})";

            return sql;
        }

        public static string CreateQuerySelect<T>(string condition = "")
        {
            var tableName = string.Empty;
            if (typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is TableAttribute attributeTable)
            {
                tableName = attributeTable.Name;
            }
            else
            {
                tableName = typeof(T).Name;
            }

            if (string.IsNullOrWhiteSpace(condition))
            {
                condition = "WHERE 1";
            }
            else
            {
                condition = $"WHERE {condition}";
            }

            var props = typeof(T).GetProperties().ToList();
            string columnNames = "";
            for (int i = 0; i < props.Count; i++)
            {
                var columnName = string.Empty;
                if (props[i].GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() is ColumnAttribute attributeColumn)
                {
                    columnName = attributeColumn.Name;
                }
                else
                {
                    columnName = props[i].Name;
                }

                columnNames += $"`{columnName}`";
                if (i != props.Count - 1)
                {
                    columnNames += ",";
                }

            }
            string sql = $"SELECT {columnNames} FROM `{tableName}` {condition}";
            return sql;
        }

        public static string CreateQueryUpdate<T>(T obj)
        {
            var tableName = string.Empty;
            if (obj.GetType().GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is TableAttribute attributeTable)
            {
                tableName = attributeTable.Name;
            }
            else
            {
                tableName = typeof(T).Name;
            }

            var props = typeof(T).GetProperties().ToList();

            string columnValues = "";
            string condition = "WHERE 1";
            for (int i = 0; i < props.Count; i++)
            {
                var columnName = string.Empty;
                if (props[i].GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() is ColumnAttribute attributeColumn)
                {
                    columnName = attributeColumn.Name;
                }
                else
                {
                    columnName = props[i].Name;
                }

                // build column values string
                if (!props[i].CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)))
                {
                    columnValues += $"`{columnName}`= {GetValue(props[i].GetValue(obj))}";
                    if (i != props.Count - 1)
                    {
                        columnValues += ",";
                    }
                }

                // build condition string
                else
                {
                    condition += $" AND `{columnName}`= {GetValue(props[i].GetValue(obj))}";

                }
            }

            string sql = $"UPDATE `{tableName}` SET {columnValues} {condition}";
            return sql;
        }

        public static string CreateQueryUpdateWithCondition<T>(T obj, string condition)
        {
            var tableName = string.Empty;
            if (obj.GetType().GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is TableAttribute attributeTable)
            {
                tableName = attributeTable.Name;
            }
            else
            {
                tableName = typeof(T).Name;
            }

            var props = typeof(T).GetProperties().ToList();

            string columnValues = "";

            if (string.IsNullOrWhiteSpace(condition))
            {
                condition = "WHERE 1";
            }
            else
            {
                condition = $"WHERE {condition}";
            }

            // build column values string
            for (int i = 0; i < props.Count; i++)
            {
                var columnName = string.Empty;
                if (props[i].GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() is ColumnAttribute attributeColumn)
                {
                    columnName = attributeColumn.Name;
                }
                else
                {
                    columnName = props[i].Name;
                }

                columnValues += $"`{columnName}`= {GetValue(props[i].GetValue(obj))}";
                if (i != props.Count - 1)
                {
                    columnValues += ",";
                }
            }

            return $"UPDATE `{tableName}` SET {columnValues} {condition}";
        }

        private static string GetValue(object valueObj)
        {
            if(valueObj == null)
            {
                return "NULL";
            }
            if(valueObj is bool)
            {
                return (bool)valueObj == true ? "1" : "0";
            }
            else if(valueObj is DateTime)
            {
                return $"'{((DateTime)valueObj).ToString("yyyyMMddHHmmss")}'";
            }
			else if (valueObj is Enum)
			{
				return $"'{((Enum)valueObj).GetHashCode()}'";
			}
			else
            {
                //Encoding utf8 = new UTF8Encoding(true);
                return $"'{valueObj.ToString().Replace("'", "''")}'";
            }
        }
    }
}
