using Common;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Web.Controllers;

namespace Web.Providers
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var candidates = GlobalConfiguration.IntegrationTemplate.ModelTemplates;

            foreach (var candidate in candidates)
            {
                var type = typeof(IntegrationBaseCommand<>).MakeGenericType(candidate.Type);
                var typeArray = typeof(IntegrationArrayBaseCommand<>).MakeGenericType(candidate.Type);
                feature.Controllers.Add(typeof(IntegrationBaseController<,,>).MakeGenericType(type, typeArray, candidate.Type).GetTypeInfo());
            }
        }
    }
}
