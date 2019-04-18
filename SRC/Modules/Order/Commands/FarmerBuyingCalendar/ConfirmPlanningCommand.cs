using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.UoMs.Interfaces;
using Order.UI.Interfaces;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.FarmerBuyingCalendar
{

    public class ConfirmPlanningCommand : BaseCommand<int>
    {
        public IEnumerable<ComfirmFarmerBuyingCalendarViewModel> Items { set; get; }
        public ConfirmPlanningCommand(IEnumerable<ComfirmFarmerBuyingCalendarViewModel> items)
        {
            Items = items;
        }
    }

    public class ConfirmPlanningCommandHandler : BaseCommandHandler<ConfirmPlanningCommand, int>
    {
        private readonly IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository = null;
        private readonly IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries = null;
        private readonly IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository = null;
        private readonly IProductQueries productQueries = null;
        private readonly IUoMQueries uoMQueries = null;
        public ConfirmPlanningCommandHandler(IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository,
                                     IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries,
                                     IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository)
        {
            this.farmerBuyingCalendarRepository = farmerBuyingCalendarRepository;
            this.farmerBuyingCalendarQueries = farmerBuyingCalendarQueries;
        }
        public override async Task<int> HandleCommand(ConfirmPlanningCommand request, CancellationToken cancellationToken)
        {
            var rs = 0;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        farmerBuyingCalendarQueries.JoinTransaction(conn, trans);
                        farmerBuyingCalendarRepository.JoinTransaction(conn, trans);

                        var list = request.Items.GroupBy(x => x.FarmerBuyingCalendarId).ToList();
                        foreach (var planningVM in list)
                        {
                            var planning = await farmerBuyingCalendarQueries.Get(planningVM.Key);
                            if (planning == null && planning.Id == 0)
                            {
                                throw new BusinessException("FarmerBuyingCalendar.NotExisted");
                            }
                            planning = UpdateBuild(planning, request.LoginSession);
                            await farmerBuyingCalendarRepository.Update(planning);
                            foreach (var itemVM in planningVM)
                            {
                                var item = planning.Items.Find(x => x.Id == itemVM.Id);

                                if (item == null || item.Id == 0)
                                {
                                    LogHelper.GetLogger().Warn(string.Format("Planning Item not found. Planning Id {0} - Planning Item Id {1}", planning.Id, itemVM.Id));
                                    continue;
                                }

                                item.AdapQuantity = itemVM.AdapQuantity;
                                item.AdapNote = itemVM.AdapNote;
                                if (await farmerBuyingCalendarRepository.UpdateItem(item) <= 0)
                                {
                                    LogHelper.GetLogger().Warn(string.Format("Update Planning Item fail. Planning Id {0} - Planning Item Id {1}", planning.Id, item.Id));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        rs = -1;
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0)
                            trans.Commit();
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

            return 0;
        }
    }
}
