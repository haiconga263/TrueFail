using Common.Interfaces;
using GS1.UI.GLN.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.GLN.Interfaces
{
	public interface IGLNQueries : IBaseQueries
	{
		Task<GLNModel> GetByIdAsync(int id);
		Task<IEnumerable<GLNModel>> GetsAsync();
		Task<IEnumerable<GLNModel>> GetAllAsync();
	}
}
