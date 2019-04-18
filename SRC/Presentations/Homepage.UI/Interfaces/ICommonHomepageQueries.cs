using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface ICommonHomepageQueries : IBaseQueries
	{
		List<string> GetAllDirectories();
	}
}
