using Common.Exceptions;
using Famrers.Commands.Commands.Famrers;
using Farmers.UI;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Farmers.ViewModels;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using MDM.UI.Settings.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Farmers.Controllers
{
    [Route(FarmerUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class FarmerController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IFarmerQueries farmerQueries = null;
        private readonly ISettingQueries settingQueries = null;

        public FarmerController(IMediator mediator, IFarmerQueries farmerQueries, ISettingQueries settingQueries)
        {
            this.mediator = mediator;
            this.farmerQueries = farmerQueries;
            this.settingQueries = settingQueries;
        }

        [HttpGet]
        [Route(FarmerUrl.gets)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerQueries.Gets()
            };
        }

        [HttpGet]
        [Route(FarmerUrl.get)]
        [AuthorizeUser("Farmer,Administrator")]
        public async Task<APIResult> Get(int farmerId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerQueries.Get(farmerId)
            };
        }

        [HttpGet]
        [Route(FarmerUrl.getByUser)]
        [AuthorizeUser("Farmer,Administrator")]
        public async Task<APIResult> GetByUser(int? userId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerQueries.GetByUser(userId ?? LoginSession.Id)
            };
        }

        [HttpPost]
        [Route(FarmerUrl.add)]
        [AuthorizeUser("Farmer,Administrator")]
        public async Task<APIResult> Add([FromBody]AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(FarmerUrl.update)]
        [AuthorizeUser("Farmer,Administrator")]
        public async Task<APIResult> Update([FromBody]UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(FarmerUrl.delete)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }


        #region App AriFarm
        [HttpGet]
        [Route(FarmerUrl.getConfig)]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> GetConfig()
        {
            Dictionary<string, string> rs = new Dictionary<string, string>();
            var settings = (await settingQueries.GetsByPrefixAsync(SettingKeys.PrefixAppAriFarm)).ToList();

            if (settings == null) settings = new List<Setting>();

            settings.ForEach(x => rs.Add(x.Name.Replace(SettingKeys.PrefixAppAriFarm + ".", ""), x.Value));

            return new APIResult() { Data = rs };
        }

        [HttpGet]
        [Route(FarmerUrl.getProfile)]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> GetProfile()
        {
            var f = await farmerQueries.GetByUser(LoginSession.Id);
            if (f == null)
                throw new BusinessException("Farmer.NotExisted");

            return new APIResult()
            {
                Result = 0,
                Data = new FarmerProfile()
                {
                    FarmerId = f.Id,
                    Name = f.Contact?.Name,
                    Email = f.Contact?.Email,
                    Gender = f.Contact?.Gender,
                    Phone = f.Contact?.Phone,
                    ImageURL = f.ImageURL,
                    CountryId = f.Address?.CountryId ?? 0,
                    DistrictId = f.Address?.DistrictId ?? 0,
                    ProvinceId = f.Address?.ProvinceId ?? 0,
                    WardId = f.Address?.WardId ?? 0,
                    Street = f.Address?.Street,
                }
            };
        }

        [HttpPost]
        [Route(FarmerUrl.updateProfile)]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> UpdateProfile([FromBody]UpdateProfileCommand command)
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
