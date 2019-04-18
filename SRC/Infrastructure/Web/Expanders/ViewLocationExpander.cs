using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Expanders
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        private const string MODULE_KEY = "module";
        /// <summary>
        /// Used to specify the locations that the view engine should search to 
        /// locate views.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            context.Values.TryGetValue(MODULE_KEY, out string module);
            if (!string.IsNullOrWhiteSpace(module))
            {
                string[] locations = new string[] {
                    "Modules/" + module + "/Views/{1}/{0}.cshtml",
                    "Modules/" + module + "/Views/Shared/{0}.cshtml",
                };
                return locations.Union(viewLocations);          //Add mvc default locations after ours
            }
            return viewLocations;
        }


        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controllerName = context.ActionContext.ActionDescriptor.DisplayName;
            var moduleName = controllerName.Split('(', ')')[1];
            context.Values[MODULE_KEY] = moduleName;

            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }
    }
}
