using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePage.Web.Models
{
	public class BlogViewModel
	{
		public int Id { get; set; }
		public string HagTag { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public DateTime DatePost { get; set; }
		public string Image { get; set; }
		public BlogTypeViewModel BlogTypeViewModels { get; set; }

	}

	public class BlogTypeViewModel
	{
		public int Id { get; set; }
		public string TypeName { get; set; }
	}
}
