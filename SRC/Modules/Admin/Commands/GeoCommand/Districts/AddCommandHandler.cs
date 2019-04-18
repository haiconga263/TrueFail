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

namespace Admin.Commands.GeoCommand.Districts
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IDistrictRepository districtRepository = null;
        private readonly IDistrictQueries districtQueries = null;
        private readonly IProvinceQueries provinceQueries = null;
        private readonly IRegionQueries regionQueries = null;
        private readonly ICountryQueries countryQueries = null;
        public AddCommandHandler(IDistrictRepository districtRepository, IDistrictQueries districtQueries, IProvinceQueries provinceQueries, IRegionQueries regionQueries, ICountryQueries countryQueries)
        {
            this.districtRepository = districtRepository;
            this.districtQueries = districtQueries;
            this.provinceQueries = provinceQueries;
            this.regionQueries = regionQueries;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.District == null || string.IsNullOrEmpty(request.District.Code))
            {
                throw new BusinessException("AddWrongInformation");
            }


            var checkingDistrict = (await districtQueries.Gets($"d.code = '{request.District.Code}' and d.is_deleted = 0")).FirstOrDefault();
            if (checkingDistrict != null)
            {
                throw new BusinessException("District.ExistedCode");
            }

            var country = await countryQueries.Get(request.District.CountryId);
            if (country == null)
            {
                throw new BusinessException("Country.NotExisted");
            }

            var province = await provinceQueries.Get(request.District.ProvinceId);
            if (province == null || province.CountryId != request.District.CountryId)
            {
                throw new BusinessException("Province.NotExisted");
            }

            request.District = CreateBuild(request.District, request.LoginSession);
            var rs = await districtRepository.Add(request.District);

            return rs == 0 ? - 1 : 0;
        }
    }
}
