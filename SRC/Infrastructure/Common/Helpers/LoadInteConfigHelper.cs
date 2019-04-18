using Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common.Helpers
{
    public class LoadInteConfigHelper
    {
        public static IntegrationTemplate Load(string fileName)
        {
            IntegrationTemplate result = new IntegrationTemplate();

            if (!File.Exists(fileName))
            {
                return null;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            var root = doc.DocumentElement;

            foreach (XmlNode area in root.ChildNodes)
            {
                if (area.Name.Equals("ModelArea"))
                {
                    var assembly = area.Attributes["assembly"]?.Value;
                    foreach (XmlNode model in area.ChildNodes)
                    {
                        if (model.Name.Equals("Model"))
                        {
                            var name = (model.Attributes["Name"] == null) ? throw new Exception("Model name must be existed") :
                                                                            ((model.Attributes["Name"].Value.Equals(string.Empty)) ? throw new Exception("Model name must be not null") :
                                                                                                                                          model.Attributes["Name"].Value);
                            var routeLink = (model.Attributes["RouteLink"] == null) ? $"api/{name}" : model.Attributes["RouteLink"].Value;

                            var inserEvent = new ModelEvent();
                            var updateEvent = new ModelEvent();
                            var deleteEvent = new ModelEvent();
                            foreach (XmlNode events in model.ChildNodes)
                            {
                                if(events.Name.Equals("InsertEvent"))
                                {
                                    inserEvent = new ModelEvent()
                                    {
                                        BeforeEvent = model.Attributes["Before"]?.Value,
                                        AfterEvent = model.Attributes["After"]?.Value,
                                        AfterRowEvent = model.Attributes["AfterRow"]?.Value,
                                        BeforeRowEvent = model.Attributes["BeforeRow"]?.Value
                                    };
                                }
                                else if (events.Name.Equals("UpdateEvent"))
                                {
                                    updateEvent = new ModelEvent()
                                    {
                                        BeforeEvent = model.Attributes["Before"]?.Value,
                                        AfterEvent = model.Attributes["After"]?.Value,
                                        AfterRowEvent = model.Attributes["AfterRow"]?.Value,
                                        BeforeRowEvent = model.Attributes["BeforeRow"]?.Value
                                    };
                                }
                                else if (events.Name.Equals("DeleteEvent"))
                                {
                                    deleteEvent = new ModelEvent()
                                    {
                                        BeforeEvent = model.Attributes["Before"]?.Value,
                                        AfterEvent = model.Attributes["After"]?.Value,
                                        AfterRowEvent = model.Attributes["AfterRow"]?.Value,
                                        BeforeRowEvent = model.Attributes["BeforeRow"]?.Value
                                    };
                                }
                            }

                            var typeStr = (model.Attributes["Type"] == null) ? throw new Exception("Model Type must be existed") : model.Attributes["Type"].Value;
                            Type type = null;
                            foreach (var module in GlobalConfiguration.Modules.Where(m => m.Name.EndsWith(".UI")))
                            {
                                type = module.Assembly.GetType(typeStr);
                                if(type != null)
                                {
                                    break;
                                }
                            }

                            if(type == null)
                            {
                                throw new Exception("Model Type must be existed");
                            }

                            result.ModelTemplates.Add(new ModelTemplate()
                            {
                                Name = name,
                                Type = type,
                                RouteLink = routeLink,
                                Insert = inserEvent,
                                Update = updateEvent,
                                Delete = deleteEvent
                            });
                        }
                    }
                    result.ModelAssembly = assembly;
                }
            }

            return result;
        }
    }
}
