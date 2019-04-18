using Common.Exceptions;
using DAL;
using MDM.UI.Roles.Interfaces;
using MDM.UI.Roles.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
namespace Administrator.Commands.RoleCommands
{
    public class InsertRoleCommand : BaseCommand<int>
    {
        public Role Model { set; get; }
        public InsertRoleCommand(Role role)
        {
            Model = role;
        }
    }

    public class InsertRoleCommandHandler : BaseCommandHandler<InsertRoleCommand, int>
    {
        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;
        public InsertRoleCommandHandler(IRoleRepository roleRepository, IRoleQueries roleQueries)
        {
            this.roleRepository = roleRepository;
            this.roleQueries = roleQueries;
        }
        public override async Task<int> HandleCommand(InsertRoleCommand request, CancellationToken cancellationToken)
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

                        id = await roleRepository.AddAsync(request.Model);
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
