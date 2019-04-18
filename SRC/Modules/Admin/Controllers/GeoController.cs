using MDM.UI.Geographical.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class GeoController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICountryQueries countryQueries = null;
        private readonly IRegionQueries regionQueries = null;
        private readonly IProvinceQueries provinceQueries = null;
        private readonly IDistrictQueries districtQueries = null;
        private readonly IWardQueries wardQueries = null;
        public GeoController(IMediator mediator,
                                  ICountryQueries countryQueries,
                                  IRegionQueries regionQueries,
                                  IProvinceQueries provinceQueries,
                                  IDistrictQueries districtQueries,
                                  IWardQueries wardQueries)
        {
            this.mediator = mediator;
            this.countryQueries = countryQueries;
            this.regionQueries = regionQueries;
            this.provinceQueries = provinceQueries;
            this.districtQueries = districtQueries;
            this.wardQueries = wardQueries;
        }

        #region Country
        [HttpGet]
        [Route("common/countries")]
        [AuthorizeUser()]
        public async Task<APIResult> GetCommonCountries()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await countryQueries.GetCommons()
            };
        }

        [HttpGet]
        [Route("get/countries")]
        public async Task<APIResult> GetCountries()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await countryQueries.Gets()
            };
        }

        [HttpGet]
        [Route("get/country")]
        public async Task<APIResult> Get(int countryId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await countryQueries.Get(countryId)
            };
        }

        [HttpPost]
        [Route("add/country")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Add([FromBody]Commands.GeoCommand.Countries.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/country")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Update([FromBody]Commands.GeoCommand.Countries.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete/country")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Delete([FromBody]Commands.GeoCommand.Countries.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion

        #region Region
        [HttpGet]
        [Route("common/regions")]
        [AuthorizeUser()]
        public async Task<APIResult> GetCommonRegions()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await regionQueries.GetCommons()
            };
        }

        [HttpGet]
        [Route("get/regions")]
        public async Task<APIResult> GetRegions()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await regionQueries.Gets()
            };
        }

        [HttpGet]
        [Route("get/region")]
        public async Task<APIResult> GetRegion(int regionId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await regionQueries.Get(regionId)
            };
        }

        [HttpPost]
        [Route("add/region")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> AddRegion([FromBody]Commands.GeoCommand.Regions.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/region")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> UpdateRegion([FromBody]Commands.GeoCommand.Regions.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete/region")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> DeleteRegion([FromBody]Commands.GeoCommand.Regions.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion

        #region Province
        [HttpGet]
        [Route("common/provinces")]
        [AuthorizeUser()]
        public async Task<APIResult> GetCommonProvinces()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await provinceQueries.GetCommons()
            };
        }

        [HttpGet]
        [Route("get/provinces")]
        public async Task<APIResult> GetProvinces()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await provinceQueries.Gets()
            };
        }

        [HttpGet]
        [Route("get/province")]
        public async Task<APIResult> GetProvince(int provinceId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await provinceQueries.Get(provinceId)
            };
        }

        [HttpPost]
        [Route("add/province")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> AddProvince([FromBody]Commands.GeoCommand.Provinces.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/province")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> UpdateProvince([FromBody]Commands.GeoCommand.Provinces.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete/province")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> DeleteProvince([FromBody]Commands.GeoCommand.Provinces.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion

        #region District
        [HttpGet]
        [Route("common/districts")]
        [AuthorizeUser()]
        public async Task<APIResult> GetCommonDistricts()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await districtQueries.GetCommons()
            };
        }

        [HttpGet]
        [Route("get/districts")]
        public async Task<APIResult> GetDistricts()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await districtQueries.Gets()
            };
        }

        [HttpGet]
        [Route("get/district")]
        public async Task<APIResult> GetDistrict(int districtId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await districtQueries.Get(districtId)
            };
        }

        [HttpPost]
        [Route("add/district")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> AddDistrict([FromBody]Commands.GeoCommand.Districts.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/district")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> UpdateDistrict([FromBody]Commands.GeoCommand.Districts.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete/district")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> DeleteDistrict([FromBody]Commands.GeoCommand.Districts.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion

        #region Ward
        [HttpGet]
        [Route("common/wards")]
        [AuthorizeUser()]
        public async Task<APIResult> GetCommonWards()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await wardQueries.GetCommons()
            };
        }

        [HttpGet]
        [Route("get/wards")]
        public async Task<APIResult> GetWards()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await wardQueries.Gets()
            };
        }

        [HttpGet]
        [Route("get/ward")]
        public async Task<APIResult> GetWard(int wardId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await wardQueries.Get(wardId)
            };
        }

        [HttpPost]
        [Route("add/ward")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> AddWard([FromBody]Commands.GeoCommand.Wards.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/ward")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> UpdateWard([FromBody]Commands.GeoCommand.Wards.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete/ward")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> DeleteWard([FromBody]Commands.GeoCommand.Wards.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion
    }
}
