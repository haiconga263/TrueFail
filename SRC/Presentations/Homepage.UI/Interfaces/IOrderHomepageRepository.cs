using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Homepage.UI.Models;

namespace Homepage.UI.Interfaces
{
	public interface IOrderHomepageRepository : IBaseRepository
	{
		Task<int> AddAsync(RetailerOrderTemp retailerOrder);
	}
}
