using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using Fulfillments.Queries;
using Fulfillments.Repositories;
using Fulfillments.UI;
using Fulfillments.UI.Interfaces;
using Fulfillments.UI.ViewModels;
using MDM.UI;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Common.Interfaces;
using MDM.UI.Common.Models;
using MDM.UI.Fulfillments.Interfaces;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Fulfillments.Commands.FCCommand
{
	public class AddCommandHandler : BaseCommandHandler<AddFcCommand, int>
	{
		private readonly IFulfillmentCollectionRepository fulfillmentRepository = null;
		private readonly ICFShippingQueries cFShippingQueries = null;
		private readonly ICFShippingRepository iCFShippingRepository = null;
		public AddCommandHandler(IFulfillmentCollectionRepository fulfillmentRepository, ICFShippingRepository iCFShippingRepository, ICFShippingQueries cFShippingQueries)
		{
			this.fulfillmentRepository = fulfillmentRepository;
			this.iCFShippingRepository = iCFShippingRepository;
			this.cFShippingQueries = cFShippingQueries;

			//this.fulfillmentQueries = fulfillmentQueries;
		}
		public override async Task<int> HandleCommand(AddFcCommand request, CancellationToken cancellationToken)
		{

			if (request.FulfillmentCollection == null || request.FulfillmentCollection.ID == 0)
			{
				throw new BusinessException("AddWrongInformation");
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						fulfillmentRepository.JoinTransaction(conn, trans);

						////Add flu-col
						request.FulfillmentCollection = CreateBuild(request.FulfillmentCollection, request.LoginSession);
						request.FulfillmentCollection.StatusId = request.FulfillmentCollection.StatusFulCols.FirstOrDefault().Id;
                        var rsFulCol = await fulfillmentRepository.Add(request.FulfillmentCollection);
                        
						if (rsFulCol >= 0)
                        {
                            //Add flu_col_item
                            if (request.FulfillmentCollection.Items.Count != 0 && request.FulfillmentCollection.StatusFulCols.FirstOrDefault().Id != 2)
                            {
                                foreach (var fcItems in request.FulfillmentCollection.Items)
                                {
                                    fcItems.ShippingId = rsFulCol;

                                    rs = (await fulfillmentRepository.AddItems(fcItems) > 0) ? 0 : -1; ;
                                }
                            }
                            if(rs != -1)
                            {
                                //Update status cf_shipping
                                var itemUp = await cFShippingQueries.Get(request.FulfillmentCollection.ID);
                                if (itemUp != null)
                                {
                                    itemUp.StatusId = 4;
                                    var cf_shpping = await iCFShippingRepository.Update(itemUp);
                                }

                            }
                        }
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
