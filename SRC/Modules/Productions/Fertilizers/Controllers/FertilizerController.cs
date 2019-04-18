using _Categories = Productions.Fertilizers.Commands.FertilizerCategories;
using _Fertilizers = Productions.Fertilizers.Commands;
using Productions.UI.Fertilizers.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Productions.UI.Fertilizers;

namespace Productions.Fertilizers.Controllers
{
    [Route(FertilizerUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class FertilizerController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IFertilizerQueries fertilizerQueries = null;
        private readonly IFertilizerCategoryQueries fertilizerCategoryQueries = null;
        public FertilizerController(IMediator mediator, IFertilizerQueries fertilizerQueries, IFertilizerCategoryQueries fertilizerCategoryQueries)
        {
            this.mediator = mediator;
            this.fertilizerQueries = fertilizerQueries;
            this.fertilizerCategoryQueries = fertilizerCategoryQueries;
        }

        [HttpGet]
        [Route(FertilizerUrl.getAlls)]
        [AuthorizeUser()]
        public async Task<APIResult> GetAlls()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fertilizerQueries.GetAll()
            };
        }

        [HttpGet]
        [Route(FertilizerUrl.get)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fertilizerQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(FertilizerUrl.gets)]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fertilizerQueries.Gets()
            };
        }

        [HttpPost]
        [Route(FertilizerUrl.insert)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Add([FromBody]_Fertilizers.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(FertilizerUrl.update)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Update([FromBody]_Fertilizers.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(FertilizerUrl.delete)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Delete([FromBody]_Fertilizers.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpGet]
        [Route(FertilizerUrl.getAllCatetories)]
        [AuthorizeUser()]
        public async Task<APIResult> GetAllCatetories()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fertilizerCategoryQueries.GetAll()
            };
        }

        [HttpGet]
        [Route(FertilizerUrl.getCatetory)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> GetCatetory(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fertilizerCategoryQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(FertilizerUrl.getCatetories)]
        [AuthorizeUser()]
        public async Task<APIResult> GetCatetories()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fertilizerCategoryQueries.Gets()
            };
        }

        [HttpPost]
        [Route(FertilizerUrl.insertCatetory)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> AddCatetory([FromBody]_Categories.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(FertilizerUrl.updateCatetory)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> UpdateCatetory([FromBody]_Categories.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(FertilizerUrl.deleteCatetory)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> DeleteCatetory([FromBody]_Categories.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
