using Common.Exceptions;
using DAL;
using MDM.UI.Wards.Interfaces;
using MDM.UI.Wards.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
namespace Administrator.Commands.WardCommands
{
    public class InsertWardCommand : BaseCommand<int>
    {
        public Ward Model { set; get; }
        public InsertWardCommand(Ward ward)
        {
            Model = ward;
        }
    }

    public class InsertWardCommandHandler : BaseCommandHandler<InsertWardCommand, int>
    {
        private readonly IWardRepository wardRepository = null;
        private readonly IWardQueries wardQueries = null;
        public InsertWardCommandHandler(IWardRepository wardRepository, IWardQueries wardQueries)
        {
            this.wardRepository = wardRepository;
            this.wardQueries = wardQueries;
        }
        public override async Task<int> HandleCommand(InsertWardCommand request, CancellationToken cancellationToken)
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

                        id = await wardRepository.AddAsync(request.Model);
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
