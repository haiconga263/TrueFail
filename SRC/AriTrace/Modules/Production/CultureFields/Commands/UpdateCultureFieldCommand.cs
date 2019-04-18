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
    public class UpdateCultureFieldCommand : BaseCommand<int>
    {
        public CultureField Model { set; get; }
        public UpdateCultureFieldCommand(CultureField cultureField)
        {
            Model = cultureField;
        }
    }

    public class UpdateCultureFieldCommandHandler : BaseCommandHandler<UpdateCultureFieldCommand, int>
    {
        private readonly ICultureFieldRepository cultureFieldRepository = null;
        private readonly ICultureFieldQueries cultureFieldQueries = null;
        public UpdateCultureFieldCommandHandler(ICultureFieldRepository cultureFieldRepository, ICultureFieldQueries cultureFieldQueries)
        {
            this.cultureFieldRepository = cultureFieldRepository;
            this.cultureFieldQueries = cultureFieldQueries;
        }
        public override async Task<int> HandleCommand(UpdateCultureFieldCommand request, CancellationToken cancellationToken)
        {
            CultureField cultureField = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("CultureField.NotExisted");
            }
            else
            {
                cultureField = await cultureFieldQueries.GetByIdAsync(request.Model.Id);
                if (cultureField == null)
                {
                    throw new BusinessException("CultureField.NotExisted");
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
                        if (await cultureFieldRepository.UpdateAsync(request.Model) > 0)
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
