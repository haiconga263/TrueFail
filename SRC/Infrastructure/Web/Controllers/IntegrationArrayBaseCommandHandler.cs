using Common;
using Common.Exceptions;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class IntegrationArrayBaseCommandHandler<T> : BaseCommandHandler<IntegrationArrayBaseCommand<T>, int>
                                                     where T : BaseModel
    {
        private IIntegrationRepository<T> repository = null;
        public IntegrationArrayBaseCommandHandler(IIntegrationRepository<T> repository)
        {
            this.repository = repository;
        }

        public async override Task<int> HandleCommand(IntegrationArrayBaseCommand<T> request, CancellationToken cancellationToken)
        {
            var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);
            if (model == null)
            {
                throw new BusinessException($"Type '{typeof(T).Name}' doesn't have in ModelTemplates");
            }

            return await repository.WriteArray(request.LoginSession, request.Value);
        }
    }
}
