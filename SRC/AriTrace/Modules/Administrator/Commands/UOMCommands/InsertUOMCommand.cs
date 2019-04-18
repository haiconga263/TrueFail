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
    public class InsertUOMCommand : BaseCommand<int>
    {
        public UOM Model { set; get; }
        public InsertUOMCommand(UOM uOM)
        {
            Model = uOM;
        }
    }

    public class InsertUOMCommandHandler : BaseCommandHandler<InsertUOMCommand, int>
    {
        private readonly IUnitOfMeasurementRepository uomRepository = null;
        private readonly IUnitOfMeasurementQueries uomQueries = null;
        public InsertUOMCommandHandler(IUnitOfMeasurementRepository uomRepository, IUnitOfMeasurementQueries uomQueries)
        {
            this.uomRepository = uomRepository;
            this.uomQueries = uomQueries;
        }
        public override async Task<int> HandleCommand(InsertUOMCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        id = await uomRepository.AddAsync(request.Model);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (id > 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }
            return id;
        }
    }
}
