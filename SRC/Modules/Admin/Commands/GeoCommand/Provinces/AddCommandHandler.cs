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

namespace Admin.Commands.GeoCommand.Provinces
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IProvinceRepository provinceRepository = null;
        private readonly IProvinceQueries provinceQueries = null;
        private readonly IRegionQueries regionQueries = null;
        private readonly ICountryQueries countryQueries = null;
        public AddCommandHandler(IProvinceRepository provinceRepository, IProvinceQueries provinceQueries, IRegionQueries regionQueries, ICountryQueries countryQueries)
        {
            this.provinceRepository = provinceRepository;
            this.provinceQueries = provinceQueries;
            this.regionQueries = regionQueries;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Province == null || string.IsNullOrEmpty(request.Province.Code))
            {
                throw new BusinessException("AddWrongInformation");
            }

            var province = (await provinceQueries.Gets($"p.code = '{request.Province.Code}' and p.is_deleted = 0")).FirstOrDefault();
            if (province != null)
            {
                throw new BusinessException("Province.ExistedCode");
            }

            var country = await countryQueries.Get(request.Province.CountryId);
            if (country == null)
            {
                throw new BusinessException("Country.NotExisted");
            }

            if(request.Province.RegionId != null)
            {
                var region = await regionQueries.Get(request.Province.RegionId.Value);
                if (region == null || region.CountryId != request.Province.CountryId)
                {
                    throw new BusinessException("Region.NotExisted");
                }
            }

            request.Province = CreateBuild(request.Province, request.LoginSession);
            var rs = await provinceRepository.Add(request.Province);

            return rs == 0 ? - 1 : 0;
        }
    }
}
