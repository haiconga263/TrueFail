using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using Homepage.UI.Interfaces;
using MDM.UI.Products.Interfaces;
using Web.Controllers;

namespace Homepage.Commands.ContactCommand
{
	public class InsertContactCommandHandler : BaseCommandHandler<InsertContactCommand, int>
	{
		private readonly IContactHomepageRepository contactRepository = null;
		public InsertContactCommandHandler(IContactHomepageRepository contactRepository)
		{
			this.contactRepository = contactRepository;
		}

		public override async Task<int> HandleCommand(InsertContactCommand request, CancellationToken cancellationToken)
		{
			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						request.Contact = CreateBuild(request.Contact, request.LoginSession);
						request.Contact.Status = Status.Read;
						var contactId = await contactRepository.AddAsync(request.Contact);

						rs = 0;

					}
					catch (Exception ex)
					{
						throw ex;
					}
					finally
					{
						if (rs == 0)
						{
							trans.Commit();
						}
						else
						{
							try
							{
								trans.Rollback();
							}
							catch { }
						}
					}
				}
			}

			return rs;
		}
	}
}
