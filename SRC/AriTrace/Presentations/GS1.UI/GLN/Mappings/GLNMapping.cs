using GS1.UI.GLN.Models;
using GS1.UI.GLN.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS1.UI.GLN.Mappings
{
	public static class GLNMapping
	{
		public static GLNDetailViewModel ToInformation(this GLNModel gln)
		{
			var serializedParent = JsonConvert.SerializeObject(gln);
			var info = JsonConvert.DeserializeObject<GLNDetailViewModel>(serializedParent);

			return info;
		}
	}
}
