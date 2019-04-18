using Common.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Queries
{
    public class IntegrationBaseQueries<T> : IIntegrationQueries<T>
    {
        public async Task<T> Get(long id)
        {
            string cmd = QueriesCreatingHelper.CreateQuerySelect<T>($"`id` = {id}");
            return (await DALHelper.ExecuteQuery<T>(cmd)).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> Gets(string condition = "")
        {
            string cmd = QueriesCreatingHelper.CreateQuerySelect<T>(condition);
            return await DALHelper.ExecuteQuery<T>(cmd);
        }
    }
}
