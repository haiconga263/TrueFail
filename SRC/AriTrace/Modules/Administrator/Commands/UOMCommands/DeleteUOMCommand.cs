using Common.Exceptions;
using DAL;
using MDM.UI.UnitOfMeasurements.Interfaces;
using MDM.UI.UnitOfMeasurements.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.UOMCommands
{
    public class DeleteUOMCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteUOMCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteUOMCommandHandler : BaseCommandHandler<DeleteUOMCommand, int>
    {
        private readonly IUnitOfMeasurementRepository uomRepository = null;
        private readonly IUnitOfMeasurementQueries uomQueries = null;
        public DeleteUOMCommandHandler(IUnitOfMeasurementRepository uomRepository, IUnitOfMeasurementQueries uomQueries)
        {
            this.uomRepository = uomRepository;
            this.uomQueries = uomQueries;
        }
        public override async Task<int> HandleCommand(DeleteUOMCommand request, CancellationToken cancellationToken)
        {
            if (request.Model == 0)
            {
                throw new BusinessException("UOM.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (await uomRepository.DeleteAsync(request.Model) > 0)
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
