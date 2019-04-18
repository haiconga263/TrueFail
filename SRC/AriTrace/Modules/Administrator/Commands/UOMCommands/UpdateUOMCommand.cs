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
    public class UpdateUOMCommand : BaseCommand<int>
    {
        public UOM Model { set; get; }
        public UpdateUOMCommand(UOM uOM)
        {
            Model = uOM;
        }
    }

    public class UpdateUOMCommandHandler : BaseCommandHandler<UpdateUOMCommand, int>
    {
        private readonly IUnitOfMeasurementRepository uomRepository = null;
        private readonly IUnitOfMeasurementQueries uomQueries = null;
        public UpdateUOMCommandHandler(IUnitOfMeasurementRepository uomRepository, IUnitOfMeasurementQueries uomQueries)
        {
            this.uomRepository = uomRepository;
            this.uomQueries = uomQueries;
        }
        public override async Task<int> HandleCommand(UpdateUOMCommand request, CancellationToken cancellationToken)
        {
            UOM uom = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("UOM.NotExisted");
            }
            else
            {
                uom = await uomQueries.GetByIdAsync(request.Model.Id);
                if (uom == null)
                {
                    throw new BusinessException("UOM.NotExisted");
                }
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (await uomRepository.UpdateAsync(request.Model) > 0)
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
