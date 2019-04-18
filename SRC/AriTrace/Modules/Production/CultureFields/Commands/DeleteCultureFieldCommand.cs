using Common.Exceptions;
using DAL;
using Production.UI.CultureFields.Interfaces;
using Production.UI.CultureFields.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.CultureFields.Commands
{
    public class DeleteCultureFieldCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteCultureFieldCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteCultureFieldCommandHandler : BaseCommandHandler<DeleteCultureFieldCommand, int>
    {
        private readonly ICultureFieldRepository cultureFieldRepository = null;
        private readonly ICultureFieldQueries cultureFieldQueries = null;
        public DeleteCultureFieldCommandHandler(ICultureFieldRepository cultureFieldRepository, ICultureFieldQueries cultureFieldQueries)
        {
            this.cultureFieldRepository = cultureFieldRepository;
            this.cultureFieldQueries = cultureFieldQueries;
        }
        public override async Task<int> HandleCommand(DeleteCultureFieldCommand request, CancellationToken cancellationToken)
        {
            CultureField cultureField = null;
            if (request.Model == 0)
            {
                throw new BusinessException("CultureField.NotSelected");
            }
            else
            {
                cultureField = await cultureFieldQueries.GetByIdAsync(request.Model);
                if (cultureField == null)
                    throw new BusinessException("CultureField.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        cultureField.IsDeleted = true;
                        cultureField.ModifiedDate = DateTime.Now;
                        cultureField.ModifiedBy = request.LoginSession.Id;

                        if (await cultureFieldRepository.UpdateAsync(cultureField) > 0)
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
