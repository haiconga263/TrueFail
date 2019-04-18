using Common.Exceptions;
using DAL;
using Production.UI.MaterialHistories.Interfaces;
using Production.UI.MaterialHistories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.MaterialHistories.Commands
{
    public class DeleteMaterialHistoryCommand : BaseCommand<int>
    {
        public int Model { get; set; }

        public DeleteMaterialHistoryCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteMaterialHistoryCommandHandler : BaseCommandHandler<DeleteMaterialHistoryCommand, int>
    {
        private readonly IMaterialHistoryRepository _materialHistoryRepository = null;
        private readonly IMaterialHistoryQueries _materialHistoryQueries = null;

        public DeleteMaterialHistoryCommandHandler(IMaterialHistoryRepository materialHistoryRepository, IMaterialHistoryQueries materialHistoryQueries)
        {
            this._materialHistoryRepository = materialHistoryRepository;
            this._materialHistoryQueries = materialHistoryQueries;
        }
        public override async Task<int> HandleCommand(DeleteMaterialHistoryCommand request, CancellationToken cancellationToken)
        {
            MaterialHistory materialHistory = null;
            if (request.Model == 0)
            {
                throw new BusinessException("MaterialHistory.NotSelected");
            }
            else
            {
                materialHistory = await _materialHistoryQueries.GetByIdAsync(request.Model);
                if (materialHistory == null)
                    throw new BusinessException("MaterialHistory.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        materialHistory.IsDeleted = true;
                        materialHistory.DeletedDate = DateTime.Now;
                        materialHistory.DeletedBy = request.LoginSession.Id;

                        if (await _materialHistoryRepository.UpdateAsync(materialHistory) > 0)
                            rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return rs;
        }
    }
}
