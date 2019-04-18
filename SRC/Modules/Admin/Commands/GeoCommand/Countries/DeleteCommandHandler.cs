using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Countries
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ICountryRepository countryRepository = null;
        public DeleteCommandHandler(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await countryRepository.Delete(DeleteBuild(new Country()
            {
                Id = request.CountryId
            }, request.LoginSession));
        }
    }
}
