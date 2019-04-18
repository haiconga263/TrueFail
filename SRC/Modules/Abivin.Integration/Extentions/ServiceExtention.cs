using System;
using System.Collections.Generic;
using System.Text;

namespace Abivin.Integration.Extentions
{
    public class ServiceExtention
    {
        public static string GetContent(string comment)
        {
            switch (comment)
            {
                case "help": // help
                    return HelpBuilder();
                default:
                    return "Comment not found";
            }
        }
        private static string HelpBuilder()
        {
            return @"
                        Command:
                            + Exit/exit: Stop service.
                            + Help/help: Show help dashboard.
                    ";
        }
    }
}
