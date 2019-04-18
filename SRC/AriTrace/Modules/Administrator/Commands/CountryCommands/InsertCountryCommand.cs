using Common.Exceptions;
using DAL;
using MDM.UI.Countries.Interfaces;
using MDM.UI.Countries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
namespace Administrator.Commands.CountryCommands
{
    public class InsertCountryCommand : BaseCommand<int>
    {
        public Country Model { set; get; }
        public InsertCountryCommand(Country country)
        {
            Model = country;
        }
    }

    public class InsertCountryCommandHandler : BaseCommandHandler<InsertCountryCommand, int>
    {
        private readonly ICountryRepository countryRepository = null;
        private readonly ICountryQueries countryQueries = null;
        public InsertCountryCommandHandler(ICountryRepository countryRepository, ICountryQueries countryQueries)
        {
            this.countryRepository = countryRepository;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(InsertCountryCommand request, CancellationToken cancellationToken)
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

                        id = await countryRepository.AddAsync(request.Model);
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
