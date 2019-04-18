using Common.Models;
using GS1.UI.Buffers.Enumerations;
using GS1.UI.SessionBuffers.Interfaces;
using GS1.UI.SessionBuffers.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Services;

namespace GS1.SessionBuffers.Services
{
    public class SessionBufferService : BaseService, ISessionBufferService
    {
        private readonly ISessionBufferQueries _sessionBufferQueries = null;
        private readonly ISessionBufferRepository _sessionBufferRepository = null;

        public SessionBufferService(ISessionBufferQueries sessionBufferQueries, ISessionBufferRepository sessionBufferRepository)
        {
            this._sessionBufferQueries = sessionBufferQueries;
            this._sessionBufferRepository = sessionBufferRepository;
        }

        public Task<IEnumerable<SessionBuffer>> GetByCompanyIdAsync(int partnerId, SessionBufferTypes sessionBufferTypes)
        {
            return _sessionBufferQueries.GetByCompanyIdAsync(partnerId, sessionBufferTypes);
        }

        public async Task<int> InsertOrUpdateAsync(SessionBuffer model, UserSession session)
        {
            var id = model.Id;
            model.ModifiedBy = session.Id;
            model.ModifiedDate = DateTime.Now;

            if (model.Id != 0)
            {
                var sessionBuffer = await _sessionBufferQueries.GetByIdAsync(model.Id);
                if (sessionBuffer == null)
                    model.Id = 0;
                else
                {
                    model.ModifiedBy = session.Id;
                    model.ModifiedDate = DateTime.Now;

                    await _sessionBufferRepository.UpdateAsync(model);
                    return id;
                }
            }

            model.CreatedBy = session.Id;
            model.CreatedDate = DateTime.Now;
            id = await _sessionBufferRepository.AddAsync(model);

            return id;
        }

    }
}
