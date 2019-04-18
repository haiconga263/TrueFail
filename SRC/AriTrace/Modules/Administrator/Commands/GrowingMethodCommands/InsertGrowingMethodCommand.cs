using Common.Exceptions;
using DAL;
using MDM.UI.GrowingMethods.Interfaces;
using MDM.UI.GrowingMethods.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
namespace Administrator.Commands.GrowingMethodCommands
{
    public class InsertGrowingMethodCommand : BaseCommand<int>
    {
        public GrowingMethod Model { set; get; }
        public InsertGrowingMethodCommand(GrowingMethod growingMethod)
        {
            Model = growingMethod;
        }
    }

    public class InsertGrowingMethodCommandHandler : BaseCommandHandler<InsertGrowingMethodCommand, int>
    {
        private readonly IGrowingMethodRepository growingMethodRepository = null;
        private readonly IGrowingMethodQueries growingMethodQueries = null;
        public InsertGrowingMethodCommandHandler(IGrowingMethodRepository growingMethodRepository, IGrowingMethodQueries growingMethodQueries)
        {
            this.growingMethodRepository = growingMethodRepository;
            this.growingMethodQueries = growingMethodQueries;
        }
        public override async Task<int> HandleCommand(InsertGrowingMethodCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        id = await growingMethodRepository.AddAsync(request.Model);
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
