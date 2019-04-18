using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using Homepage.Repositories;
using Homepage.UI.Interfaces;
using Web.Controllers;

namespace Homepage.Commands.HomeCommand
{
	public class InsertOrderCommandHandler : BaseCommandHandler<InsertOrderCommand, int>
	{
		//private readonly IOrderHomepageRepository orderRepository = null;
		//public InsertOrderCommandHandler(IOrderHomepageRepository orderRepository)
		//{
		//	this.orderRepository = orderRepository;
		//}
		//public override async Task<int> HandleCommand(InsertOrderCommand request, CancellationToken cancellationToken)
		//{
		//	var id = 0;
		//	using (var conn = DALHelper.GetConnection())
		//	{
		//		conn.Open();
		//		using (var trans = conn.BeginTransaction())
		//		{
		//			try
		//			{
		//				id = await orderRepository.AddAsync(request.OrderTemp);
		//			}
		//			catch (Exception ex)
		//			{
		//				throw ex;
		//			}
		//			finally
		//			{
		//				if (id > 0) { trans.Commit(); }
		//				else { try { trans.Rollback(); } catch { } }
		//			}
		//		}
		//	}

		//	return id;
		//}
		public override Task<int> HandleCommand(InsertOrderCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
