using _Categories = Productions.Pesticides.Commands.PesticideCategories;
using _Pesticides = Productions.Pesticides.Commands;
using Productions.UI.Pesticides.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Productions.UI.Pesticides;

namespace Productions.Pesticides.Controllers
{
    [Route(PesticideUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class PesticideController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IPesticideQueries pesticideQueries = null;
        private readonly IPesticideCategoryQueries pesticideCategoryQueries = null;
        public PesticideController(IMediator mediator, IPesticideQueries pesticideQueries, IPesticideCategoryQueries pesticideCategoryQueries)
        {
            this.mediator = mediator;
            this.pesticideQueries = pesticideQueries;
            this.pesticideCategoryQueries = pesticideCategoryQueries;
        }

        [HttpGet]
        [Route(PesticideUrl.getAlls)]
        [AuthorizeUser()]
        public async Task<APIResult> GetAlls()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await pesticideQueries.GetAll()
            };
        }

        [HttpGet]
        [Route(PesticideUrl.get)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await pesticideQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(PesticideUrl.gets)]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await pesticideQueries.Gets()
            };
        }

        [HttpPost]
        [Route(PesticideUrl.insert)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Add([FromBody]_Pesticides.AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(PesticideUrl.update)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Update([FromBody]_Pesticides.UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(PesticideUrl.delete)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Delete([FromBody]_Pesticides.DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }


        [HttpGet]
        [Route(PesticideUrl.getAllCatetories)]
        [AuthorizeUser()]
        public async Task<APIResult> GetAllCatetories()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await pesticideCategoryQueries.GetAll()
            };
        }

        [HttpGet]
        [Route(PesticideUrl.getCatetory)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> GetCatetory(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await pesticideCategoryQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(PesticideUrl.getCatetories)]
        [AuthorizeUser()]
        public async Task<APIResult> GetCatetories()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await pesticideCategoryQueries.Gets()
            };
        }

        [HttpPost]
        [Route(PesticideUrl.insertCatetory)]
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
        [Route(PesticideUrl.updateCatetory)]
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
        [Route(PesticideUrl.deleteCatetory)]
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
