using DAL;
using GS1.UI.GLN.Interfaces;
using GS1.UI.GLN.Models;
using GS1.UI.GLN.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.GLN.Commands
{
	public class InsertGLNCommand : BaseCommand<int>
	{
		public GLNModel Model { set; get; }
		public InsertGLNCommand(GLNModel gln)
		{
			Model = gln;
		}
	}

	public class InsertGLNCommandHandler : BaseCommandHandler<InsertGLNCommand, int>
	{
		private readonly IGLNRepository glnRepository = null;
		private readonly IGLNQueries glnQueries = null;
		public InsertGLNCommandHandler(IGLNRepository glnRepository, IGLNQueries glnQueries)
		{
			this.glnRepository = glnRepository;
			this.glnQueries = glnQueries;
		}
		public override async Task<int> HandleCommand(InsertGLNCommand request, CancellationToken cancellationToken)
		{
			var id = 0;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						request.Model.CreatedDate = DateTime.Now;
						request.Model.CreatedBy = request.LoginSession.Id;
						request.Model.ModifiedDate = DateTime.Now;
						request.Model.ModifiedBy = request.LoginSession.Id;

						id = await glnRepository.AddAsync(request.Model);
					}
					catch (Exception ex)
					{
						throw ex;
					}
					finally
					{
						if (id > 0) { trans.Commit(); }
						else { try { trans.Rollback(); } catch { } }
					}
				}
			}

			return id;
		}
	}
}