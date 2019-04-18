using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
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
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ICountryRepository countryRepository = null;
        private readonly ICountryQueries countryQueries = null;
        public AddCommandHandler(ICountryRepository countryRepository, ICountryQueries countryQueries)
        {
            this.countryRepository = countryRepository;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Country == null || string.IsNullOrEmpty(request.Country.Code))
            {
                throw new BusinessException("AddWrongInformation");
            }

            var country = (await countryQueries.Gets($"code = '{request.Country.Code}' and is_deleted = 0")).FirstOrDefault();
            if (country != null)
            {
                throw new BusinessException("Country.ExistedCode");
            }

            request.Country = CreateBuild(request.Country, request.LoginSession);
            var rs = await countryRepository.Add(request.Country);

            return rs == 0 ? - 1 : 0;
        }
    }
}
