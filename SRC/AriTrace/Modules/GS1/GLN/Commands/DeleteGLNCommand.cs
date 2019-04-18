using Common.Exceptions;
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
	public class DeleteGLNCommand : BaseCommand<int>
	{
		public int Model { get; set; }
		public DeleteGLNCommand(int id)
		{
			Model = id;
		}
	}

	public class DeleteGLNCommandHandler : BaseCommandHandler<DeleteGLNCommand, int>
	{
		private readonly IGLNRepository glnRepository = null;
		private readonly IGLNQueries glnQueries = null;
		public DeleteGLNCommandHandler(IGLNRepository glnRepository, IGLNQueries glnQueries)
		{
			this.glnRepository = glnRepository;
			this.glnQueries = glnQueries;
		}
		public override async Task<int> HandleCommand(DeleteGLNCommand request, CancellationToken cancellationToken)
		{
			GLNModel gln = null;
			if (request.Model == 0)
			{
				throw new BusinessException("GLN.NotSelected");
			}
			else
			{
				gln = await glnQueries.GetByIdAsync(request.Model);
				if (gln == null)
					throw new BusinessException("GLN.NotSelected");
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						gln.IsDeleted = true;
						gln.ModifiedDate = DateTime.Now;
						gln.ModifiedBy = request.LoginSession.Id;

						if (await glnRepository.UpdateAsync(gln) > 0)
							rs = 0;
					}
					catch (Exception ex)
					{
						throw ex;
					}
					finally
					{
						if (rs == 0) { trans.Commit(); }
						else { try { trans.Rollback(); } catch { } }
					}
				}
			}

			return rs;
		}
	}
}
