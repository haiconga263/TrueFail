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
    public class InsertCultureFieldCommand : BaseCommand<int>
    {
        public CultureField Model { set; get; }
        public InsertCultureFieldCommand(CultureField cultureField)
        {
            Model = cultureField;
        }
    }

    public class InsertCultureFieldCommandHandler : BaseCommandHandler<InsertCultureFieldCommand, int>
    {
        private readonly ICultureFieldRepository cultureFieldRepository = null;
        private readonly ICultureFieldQueries cultureFieldQueries = null;
        public InsertCultureFieldCommandHandler(ICultureFieldRepository cultureFieldRepository, ICultureFieldQueries cultureFieldQueries)
        {
            this.cultureFieldRepository = cultureFieldRepository;
            this.cultureFieldQueries = cultureFieldQueries;
        }
        public override async Task<int> HandleCommand(InsertCultureFieldCommand request, CancellationToken cancellationToken)
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

                        id = await cultureFieldRepository.AddAsync(request.Model);
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
