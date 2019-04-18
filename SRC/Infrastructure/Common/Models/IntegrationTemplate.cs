using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class IntegrationTemplate
    {
        public List<ModelTemplate> ModelTemplates { set; get; } = new List<ModelTemplate>();
        public string ModelAssembly { set; get; }
    }
}
