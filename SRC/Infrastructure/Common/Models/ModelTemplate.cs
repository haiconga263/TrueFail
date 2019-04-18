using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ModelTemplate
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public Type Type { set; get; }
        public string RouteLink { set; get; }

        public ModelEvent Insert { set; get; } = new ModelEvent();
        public ModelEvent Update { set; get; } = new ModelEvent();
        public ModelEvent Delete { set; get; } = new ModelEvent();
    }
}
