using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Web.Attributes;

namespace Web.Conventions
{
    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[2];

                var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Type == genericType);

                if(model != null)
                {
                    controller.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(model.RouteLink)),
                    });
                }
                else
                {
                    controller.ControllerName = model.Name;
                }
            }
        }
    }
}
