using Common.Interfaces;
using GS1.UI.GLN.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.GLN.Interfaces
{
	public interface IGLNRepository : IBaseRepository
	{
		Task<int> AddAsync(GLNModel gln);
		Task<int> UpdateAsync(GLNModel gln);
		Task<int> DeleteAsync(int id);
	}
}
