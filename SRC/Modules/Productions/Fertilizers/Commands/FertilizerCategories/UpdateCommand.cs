using Productions.UI.Fertilizers.Models;
using Web.Controllers;

namespace Productions.Fertilizers.Commands.FertilizerCategories
{
    public class UpdateCommand : BaseCommand<int>
    {
        public FertilizerCategory FertilizerCategory { set; get; }
        public UpdateCommand(FertilizerCategory fertilizerCategory)
        {
            FertilizerCategory = fertilizerCategory;
        }
    }
}
