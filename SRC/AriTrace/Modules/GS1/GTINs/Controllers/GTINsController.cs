using GS1.GTINs.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace GS1.Controllers
{
    [Route("api/gtin")]
    [AuthorizeInUserService]
    public class GTINsController : BaseController
    {
        private readonly IMediator mediator = null;


        public GTINsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("generate")]
        public async Task<APIResult> GenerateGTIN([FromBody]GenerateGTINCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("checknew")]
        public async Task<APIResult> CheckNewGTIN([FromBody]CheckNewGTINCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("calculatecheckdigit")]
        public async Task<APIResult> CalculateCheckDigitGTIN([FromBody]CalculateCheckDigitGTINCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("insertorupdate")]
        public async Task<APIResult> InsertOrUpdateGTIN([FromBody]InsertOrUpdateGTINCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }
    }
}
