using Common;
using Common.Exceptions;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class IntegrationBaseCommandHandler<T> : BaseCommandHandler<IntegrationBaseCommand<T>, int> where T: BaseModel
    {
        private IIntegrationRepository<T> repository = null;
        private IIntegrationQueries<T> queries = null;
        public IntegrationBaseCommandHandler(IIntegrationRepository<T> repository, IIntegrationQueries<T> queries)
        {
            this.repository = repository;
            this.queries = queries;
        }

        public async override Task<int> HandleCommand(IntegrationBaseCommand<T> request, CancellationToken cancellationToken)
        {
            //0: success; -1: error; 1: BadRequest.

            //todo check data rule
            var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);
            if (model == null)
            {
                throw new BusinessException($"Type '{typeof(T).Name}' doesn't have in ModelTemplates");
            }

            T value = null;
            long id = 0;

            switch(request.Type)
            {
                case IntergrationHandleType.Insert:
                    request.Value = CreateBuild(request.Value, request.LoginSession);
                    return await repository.Write(request.LoginSession, request.Value);
                case IntergrationHandleType.Update:
                    id = (int)typeof(T).GetProperty("Id").GetValue(request.Value);
                    value = await queries.Get(id);
                    if(value == null)
                    {
                        throw new BusinessException($" {typeof(T).Name} with value: {id} doesn't existed");
                    }
                    request.Value = UpdateBuild(request.Value, request.LoginSession);
                    request.Value.CreatedBy = value.CreatedBy;
                    request.Value.CreatedDate = value.CreatedDate;
                    return await repository.Update(request.LoginSession, request.Value);
                case IntergrationHandleType.Delete:
                    id = (int)typeof(T).GetProperty("Id").GetValue(request.Value);
                    value = await queries.Get(id);
                    if (value == null)
                    {
                        throw new BusinessException($" {typeof(T).Name} with value: {id} doesn't existed");
                    }
                    request.Value.CreatedBy = value.CreatedBy;
                    request.Value.CreatedDate = value.CreatedDate;
                    request.Value = DeleteBuild(request.Value, request.LoginSession);
                    return await repository.Delete(request.LoginSession, request.Value);
                default:
                    return -1;
            }
        }
    }
}
