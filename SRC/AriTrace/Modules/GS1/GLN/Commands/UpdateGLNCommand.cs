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
	public class UpdateGLNCommand : BaseCommand<int>
	{
		public GLNModel Model { set; get; }
		public UpdateGLNCommand(GLNModel gln)
		{
			Model = gln;
		}
	}

	public class UpdateCategoryCommandHandler : BaseCommandHandler<UpdateGLNCommand, int>
	{
		private readonly IGLNRepository glnRepository = null;
		private readonly IGLNQueries glnQueries = null;
		public UpdateCategoryCommandHandler(IGLNRepository glnRepository, IGLNQueries glnQueries)
		{
			this.glnRepository = glnRepository;
			this.glnQueries = glnQueries;
		}
		public override async Task<int> HandleCommand(UpdateGLNCommand request, CancellationToken cancellationToken)
		{
			GLNModel gln = null;
			if (request.Model == null || request.Model.Id == 0)
			{
				throw new BusinessException("GLN.NotExisted");
			}
			else
			{
				gln = await glnQueries.GetByIdAsync(request.Model.Id);
				if (gln == null)
				{
					throw new BusinessException("GLN.NotExisted");
				}
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						request.Model.ModifiedDate = DateTime.Now;
						request.Model.ModifiedBy = request.LoginSession.Id;
						if (await glnRepository.UpdateAsync(request.Model) > 0)
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
