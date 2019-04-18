using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Countries
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ICountryRepository countryRepository = null;
        private readonly ICountryQueries countryQueries = null;
        public UpdateCommandHandler(ICountryRepository countryRepository, ICountryQueries countryQueries)
        {
            this.countryRepository = countryRepository;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Country == null || request.Country.Id == 0)
            {
                throw new BusinessException("Country.NotExisted");
            }

            var country = await countryQueries.Get(request.Country.Id);
            if(country == null)
            {
                throw new BusinessException("Country.NotExisted");
            }

            var checkingCountry = (await countryQueries.Gets($"code = '{request.Country.Code}' and id <> {country.Id} and is_deleted = 0")).FirstOrDefault();
            if (checkingCountry != null)
            {
                throw new BusinessException("Country.ExistedCode");
            }

            country = UpdateBuild(country, request.LoginSession);
            country.Code = request.Country.Code;
            country.Name = request.Country.Name;
            country.PhoneCode = request.Country.PhoneCode;
            country.IsUsed = request.Country.IsUsed;
            var rs = await countryRepository.Update(country);

            return rs;
        }
    }
}
