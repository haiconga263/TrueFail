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
    public class UpdateRoleCommand : BaseCommand<int>
    {
        public Role Model { set; get; }
        public UpdateRoleCommand(Role role)
        {
            Model = role;
        }
    }

    public class UpdateRoleCommandHandler : BaseCommandHandler<UpdateRoleCommand, int>
    {
        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;
        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IRoleQueries roleQueries)
        {
            this.roleRepository = roleRepository;
            this.roleQueries = roleQueries;
        }
        public override async Task<int> HandleCommand(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Role.NotExisted");
            }
            else
            {
                role = await roleQueries.GetByIdAsync(request.Model.Id);
                if (role == null)
                {
                    throw new BusinessException("Role.NotExisted");
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
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        if (await roleRepository.UpdateAsync(request.Model) > 0)
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
