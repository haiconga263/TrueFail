using Common;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;using System.Text;

namespace Web.Controls
{
    public class GridDataSource<T>
    {
        public List<T> DataSource { get; set; }
        public long TotalRows { get; set; }
        public DataSourceResult DataSourceResult { get; set; }
    }

    public static class GridDataSourceExtension
    {
        public static GridDataSource<T> To<T>(this List<T> list)
        {
            long totalRow = 0;
            if (list.Count > 0)
            {
                dynamic obj = list[0];
                if (obj.GetType().GetProperty("TotalRows") != null)
                    totalRow = obj.TotalRows;
            }

            DataSourceResult dataSource = new DataSourceResult()
            {
                Data = list,
                Total = (int)totalRow
            };

            return new GridDataSource<T>()
            {
                DataSource = list,
                TotalRows = totalRow,
                DataSourceResult = dataSource
            };
        }

        public static GridDataSource<TDestination> ToGridDataSource<TSource, TDestination>(this List<TSource> source)
        {
            return source.To<TSource, TDestination>().To();
        }

        public static JsonResult ToJsonDataSource(this object data)
        {
            return new JsonResult(data);
        }
    }
}
