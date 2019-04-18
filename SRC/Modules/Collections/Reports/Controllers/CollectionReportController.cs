using Collections.UI;
using Collections.UI.Reports.ViewModels;
using Common;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Web.Helpers;

namespace Collections.Reports.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class CollectionReportController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICollectionQueries collectionQueries = null;
        private readonly ICollectionInventoryHistoryQueries collectionInventoryHistoryQueries = null;
        private readonly ICFShippingQueries cFShippingQueries = null;
        public CollectionReportController(IMediator mediator, ICollectionQueries collectionQueries, ICollectionInventoryHistoryQueries collectionInventoryHistoryQueries, ICFShippingQueries cFShippingQueries)
        {
            this.mediator = mediator;
            this.collectionQueries = collectionQueries;
            this.collectionInventoryHistoryQueries = collectionInventoryHistoryQueries;
            this.cFShippingQueries = cFShippingQueries;
        }

        [HttpGet]
        [Route("gets/completed")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetOrderHistories(DateTime from, DateTime to, int collectionId = 0)
        {
            var param = $"?from={from}&to={to}&collectionId={collectionId}";
            var orders = await WebHelper.HttpGet<IEnumerable<FarmerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetCompletedFarmerOrderByCollection}{param}", LoginSession.AccessToken);
            return new APIResult()
            {
                Result = 0,
                Data = orders
            };
        }

        [HttpGet]
        [Route("gets/completed/by-employee")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetOrderHistoriesByEmployee(DateTime from, DateTime to, int collectionId = 0)
        {
            var param = $"?from={from}&to={to}&collectionId={collectionId}";
            var orders = await WebHelper.HttpGet<IEnumerable<FarmerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetCompletedFarmerOrderByCollection}{param}", LoginSession.AccessToken);
            var reportByEmployeeses = new List<ReportByEmployeeViewModel>();
            foreach (var order in orders)
            {
                if (order.ModifiedBy == null) continue;
                var employee = reportByEmployeeses.FirstOrDefault(e => e.EmployeeId == order.ModifiedBy);
                if(employee == null)
                {
                    employee = new ReportByEmployeeViewModel()
                    {
                        EmployeeId = order.ModifiedBy.Value
                    };
                    reportByEmployeeses.Add(employee);
                }
                employee.TotalOrder += 1;
                employee.TotalAmount += order.TotalAmount;
                employee.CanceledCount += order.StatusId == (int)Order.UI.FarmerOrderStatuses.Canceled ? 1 : 0;
                employee.CompletedCount += order.StatusId == (int)Order.UI.FarmerOrderStatuses.Completed ? 1 : 0;
            }
            return new APIResult()
            {
                Result = 0,
                Data = reportByEmployeeses
            };
        }

        [HttpGet]
        [Route("gets/inventory")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetInventoryHistories(DateTime from, DateTime to, int collectionId = 0)
        {
            var condition = $"collection_id = {collectionId} and created_date > '{from.Date.ToString("yyyyMMdd")}' and created_date <= '{to.AddDays(1).Date.ToString("yyyyMMdd")}'";
            return new APIResult()
            {
                Result = 0,
                Data = await collectionInventoryHistoryQueries.Gets(condition)
            };
        }

        [HttpGet]
        [Route("gets/shipping")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetShippingHistories(DateTime from, DateTime to, int collectionId = 0)
        {
            var condition = $@"collection_id = {collectionId} and created_date > '{from.Date.ToString("yyyyMMdd")}' 
                               and created_date <= '{to.AddDays(1).Date.ToString("yyyyMMdd")}' 
                               and status_id in ({(int)Distributions.UI.TripStatuses.Finished},{(int)Distributions.UI.TripStatuses.Canceled})";
            return new APIResult()
            {
                Result = 0,
                Data = await cFShippingQueries.Gets(condition)
            };
        }
    }
}

