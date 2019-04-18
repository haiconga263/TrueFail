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
    public class DeleteRoleCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteRoleCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteRoleCommandHandler : BaseCommandHandler<DeleteRoleCommand, int>
    {
        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;
        public DeleteRoleCommandHandler(IRoleRepository roleRepository, IRoleQueries roleQueries)
        {
            this.roleRepository = roleRepository;
            this.roleQueries = roleQueries;
        }
        public override async Task<int> HandleCommand(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Role.NotSelected");
            }
            else
            {
                role = await roleQueries.GetByIdAsync(request.Model);
                if (role == null)
                    throw new BusinessException("Role.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        role.IsDeleted = true;
                        role.ModifiedDate = DateTime.Now;
                        role.ModifiedBy = request.LoginSession.Id;

                        if (await roleRepository.UpdateAsync(role) > 0)
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
