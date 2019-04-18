using Common;
using Common.Exceptions;
using DAL;
using Homepage.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.ContactCommand
{
	public class UpdateContactCommandHandler : BaseCommandHandler<UpdateContactCommand, int>
	{
		private readonly IContactHomepageRepository contactRepository = null;
		private readonly IContactHomepageQueries contactQueries = null;
		public UpdateContactCommandHandler(IContactHomepageRepository contactRepository, IContactHomepageQueries contactQueries)
		{
			this.contactRepository = contactRepository;
			this.contactQueries = contactQueries;
		}
		public override async Task<int> HandleCommand(UpdateContactCommand request, CancellationToken cancellationToken)
		{
			if (request.Contact == null || request.Contact.Id == 0)
			{
				throw new BusinessException("Contact.NotExisted");
			}

			var contact = (await contactQueries.GetContactById(request.Contact.Id));
			if (contact == null)
			{
				throw new BusinessException("Contact.NotExisted");
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						//request.Contact.CreatedDate = contact.CreatedDate;
						//request.Contact.CreatedBy = contact.CreatedBy;
						request.Contact = UpdateBuild(request.Contact, request.LoginSession);
						request.Contact.Status = Status.Read;
						rs = await contactRepository.UpdateAsync(request.Contact);

						if (rs != 0)
						{
							return -1;
						}
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
