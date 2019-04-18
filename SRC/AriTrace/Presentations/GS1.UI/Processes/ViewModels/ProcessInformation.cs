using GS1.UI.Processes.Models;
using GS1.UI.Productions.ViewModels;

namespace GS1.UI.Processes.ViewModels
{
    public class ProcessInformation : Process
    {
        public ProductionInformation Production { get; set; }
    }
}
