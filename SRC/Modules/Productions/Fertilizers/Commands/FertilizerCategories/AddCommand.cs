using Productions.UI.Fertilizers.Models;
using Web.Controllers;

namespace Productions.Fertilizers.Commands.FertilizerCategories
{
    public class AddCommand : BaseCommand<int>
    {
        public FertilizerCategory FertilizerCategory { set; get; }
        public AddCommand(FertilizerCategory fertilizerCategory)
        {
            FertilizerCategory = fertilizerCategory;
        }
    }
}
