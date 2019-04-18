import { Component, ElementRef, Injector } from '@angular/core';
import { DashBoardReportService } from './dashboard.service';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

// import { environment, functions } from 'environments/environment';
// import { fadeInAnimation } from "../../../core/route-animation/route.animation";
// import { MainService } from '../../../core/layout/service/main.service'
// import { Router } from "@angular/router";

// import { DashBoardReportService } from '../services/index'

// import { DashboardReport, ReportBase, ReportChartBase, DataSourceChart } from '../models/index'

// import * as $ from 'jquery';
// //import * as $ from 'bootstrap';

// import ChartType from "fusioncharts/viz/bar2d";
// import { FunctionsClass } from 'environments/environment';


@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent extends AppBaseComponent {
    width = "100%";
    height = 300;
    type = 'bar2d';
    dataFormat = 'json';

    private timeType: any[] = [];

    private selectvalue: string;

    // private salesOrderReport: DashboardReport = new DashboardReport();
    // private salesOrderReportBy: ReportBase = new ReportBase();

    // private salesOrderCancelReport: DashboardReport = new DashboardReport();
    // private salesOrderCancelReportBy: ReportBase = new ReportBase();

    // private salesOrderInprogressReport: DashboardReport = new DashboardReport();
    // private salesOrderInprogressReportBy: ReportBase = new ReportBase();

    // private salesOrderAmountReport: DashboardReport = new DashboardReport();
    // private salesOrderAmountReportBy: ReportBase = new ReportBase();

    // private buyOrderAmountReport: DashboardReport = new DashboardReport();
    // private buyOrderAmountReportBy: ReportBase = new ReportBase();

    // private moneyAmountReportBy: ReportBase = new ReportBase();

    // private menuCountChartReport: DashboardReport = new DashboardReport();
    // private menuAmountChartReport: DashboardReport = new DashboardReport();

    // idMenuCountChart = 'MenuCountChart';
    // menuCountDataSource: DataSourceChart = {
    //     chart: {
    //         "caption": "Số lượng đặt hàng theo món ăn",
    //         "subcaption": "(09-08-2018)",
    //         "yaxisname": "Số lượng (tô)",
    //         "numbersuffix": "",
    //         "basefontsize": "12",
    //         "basefontcolor": "#194920",
    //         "valuefontcolor": "#194920",
    //         "canvasBgColor": "#f3f5f6",
    //         "canvasBgAlpha": "100",
    //         "bgColor": "#f3f5f6",
    //         "BgAlpha": "100",
    //         "palettecolors": "#3A803D",
    //         "plottooltext": "$label Rate of Unemployment : $datavalue",
    //         "theme": "zune"
    //     },
    //     data: [{
    //         value: 0
    //     }]
    // };

    // idMenuAmountChart = "MenuAmountChart";
    // menuAmountDataSource: DataSourceChart = {
    //     chart: {
    //         "caption": "Số lượng tiền theo món ăn",
    //         "subcaption": "(09-08-2018)",
    //         "yaxisname": "Tiền (x1000 VND)",
    //         "numbersuffix": "",
    //         "basefontsize": "12",
    //         "basefontcolor": "#194920",
    //         "valuefontcolor": "#194920",
    //         "canvasBgColor": "#f3f5f6",
    //         "canvasBgAlpha": "100",
    //         "bgColor": "#f3f5f6",
    //         "BgAlpha": "100",
    //         "palettecolors": "#3A803D",
    //         "plottooltext": "$label Rate of Unemployment : $datavalue",
    //         "theme": "zune"
    //     },
    //     data: [{
    //         value: 0
    //     }]
    // };

    constructor(
        injector: Injector,
        private dashBoardReportService: DashBoardReportService, 
        private elementRef: ElementRef) {
        super(injector);
    }

    async ngOnInit() {
        await this.load();
    }

    async ngAfterViewInit() {
    }
    async load() {
        this.timeType = this.dashBoardReportService.getTimeType();
        // this.salesOrderReport = (await this.dashBoardReportService.salesOrderReport()).data;
        // this.salesOrderCancelReport = (await this.dashBoardReportService.salesOrderCancelReport()).data;
        // this.salesOrderInprogressReport = (await this.dashBoardReportService.salesOrderInprogressReport()).data;

        // this.salesOrderAmountReport = (await this.dashBoardReportService.salesOrderAmountReport()).data;
        // this.buyOrderAmountReport = (await this.dashBoardReportService.buyOrderAmountReport()).data;

        // this.menuCountChartReport = (await this.dashBoardReportService.MenuCountChartReport()).data;
        // this.menuAmountChartReport = (await this.dashBoardReportService.MenuAmountChartReport()).data;

        if (this.timeType.length > 0) {
            this.selectvalue = this.timeType[0].id.toString();
            this.onChageSelectTime(this.selectvalue);
        }

    }

    onChageSelectTime(value) {
        this.onchageSalesOrderReport(value);
        this.onchageSalesOrderCancelReport(value);
        this.onchageSalesOrderInprogressReport(value);
        this.onchageSalesOrderAmountReport(value);
        this.onchageBuyOrderAmountReport(value);
        this.onchageMoneyCountReport(value);
        this.onchageMenuCountChartReport(value);
        this.onchageMenuAmountChartReport(value);
    }

    onchageSalesOrderReport(value) {
        // switch (value) {
        //     case "1":
        //         this.salesOrderReportBy = this.salesOrderReport.byDay;
        //         break;
        //     case "2":
        //         this.salesOrderReportBy = this.salesOrderReport.byWeek;
        //         break;
        //     case "3":
        //         this.salesOrderReportBy = this.salesOrderReport.byMonth;
        //         break;
        //     case "4":
        //         this.salesOrderReportBy = this.salesOrderReport.byQuarter;
        //         break;
        //     case "5":
        //         this.salesOrderReportBy = this.salesOrderReport.byYear;
        //         break;
        // }
    }
    onchageSalesOrderCancelReport(value) {
        // switch (value) {
        //     case "1":
        //         this.salesOrderCancelReportBy = this.salesOrderCancelReport.byDay;
        //         break;
        //     case "2":
        //         this.salesOrderCancelReportBy = this.salesOrderCancelReport.byWeek;
        //         break;
        //     case "3":
        //         this.salesOrderCancelReportBy = this.salesOrderCancelReport.byMonth;
        //         break;
        //     case "4":
        //         this.salesOrderCancelReportBy = this.salesOrderCancelReport.byQuarter;
        //         break;
        //     case "5":
        //         this.salesOrderCancelReportBy = this.salesOrderCancelReport.byYear;
        //         break;
        // }
    }
    onchageSalesOrderInprogressReport(value) {
        // switch (value) {
        //     case "1":
        //         this.salesOrderInprogressReportBy = this.salesOrderInprogressReport.byDay;
        //         break;
        //     case "2":
        //         this.salesOrderInprogressReportBy = this.salesOrderInprogressReport.byWeek;
        //         break;
        //     case "3":
        //         this.salesOrderInprogressReportBy = this.salesOrderInprogressReport.byMonth;
        //         break;
        //     case "4":
        //         this.salesOrderInprogressReportBy = this.salesOrderInprogressReport.byQuarter;
        //         break;
        //     case "5":
        //         this.salesOrderInprogressReportBy = this.salesOrderInprogressReport.byYear;
        //         break;
        // }
    }
    onchageSalesOrderAmountReport(value) {
        // switch (value) {
        //     case "1":
        //         this.salesOrderAmountReportBy = this.salesOrderAmountReport.byDay;
        //         break;
        //     case "2":
        //         this.salesOrderAmountReportBy = this.salesOrderAmountReport.byWeek;
        //         break;
        //     case "3":
        //         this.salesOrderAmountReportBy = this.salesOrderAmountReport.byMonth;
        //         break;
        //     case "4":
        //         this.salesOrderAmountReportBy = this.salesOrderAmountReport.byQuarter;
        //         break;
        //     case "5":
        //         this.salesOrderAmountReportBy = this.salesOrderAmountReport.byYear;
        //         break;
        // }
    }
    onchageMoneyCountReport(value) {
        // this.moneyAmountReportBy.current = this.salesOrderAmountReportBy.current - this.buyOrderAmountReportBy.current;
        // this.moneyAmountReportBy.previous = this.salesOrderAmountReportBy.previous - this.buyOrderAmountReportBy.previous;

        // if (this.moneyAmountReportBy.previous > 0) {
        //     this.moneyAmountReportBy.growthPercen = (this.moneyAmountReportBy.current - this.moneyAmountReportBy.previous) * 100 / this.moneyAmountReportBy.previous;
        // }
        // else if (this.moneyAmountReportBy.previous == 0) {
        //     if (this.moneyAmountReportBy.current > 0) {
        //         this.moneyAmountReportBy.growthPercen = 100;
        //     }
        //     else if (this.moneyAmountReportBy.current < 0) {
        //         this.moneyAmountReportBy.growthPercen = -100;
        //     }
        //     else {
        //         this.moneyAmountReportBy.growthPercen = 0;
        //     }
        // }
        // else if (this.moneyAmountReportBy.previous < 0) {
        //     if (this.moneyAmountReportBy.current < 0) {
        //         this.moneyAmountReportBy.growthPercen = (this.moneyAmountReportBy.current - this.moneyAmountReportBy.previous) * 100 / this.moneyAmountReportBy.previous;
        //     }
        //     else {
        //         this.moneyAmountReportBy.growthPercen = 0 - ((this.moneyAmountReportBy.current - this.moneyAmountReportBy.previous) * 100 / this.moneyAmountReportBy.previous);
        //     }
        // }
    }
    onchageBuyOrderAmountReport(value) {
        // switch (value) {
        //     case "1":
        //         this.buyOrderAmountReportBy = this.buyOrderAmountReport.byDay;
        //         break;
        //     case "2":
        //         this.buyOrderAmountReportBy = this.buyOrderAmountReport.byWeek;
        //         break;
        //     case "3":
        //         this.buyOrderAmountReportBy = this.buyOrderAmountReport.byMonth;
        //         break;
        //     case "4":
        //         this.buyOrderAmountReportBy = this.buyOrderAmountReport.byQuarter;
        //         break;
        //     case "5":
        //         this.buyOrderAmountReportBy = this.buyOrderAmountReport.byYear;
        //         break;
        // }
    }

    onchageMenuCountChartReport(value) {
        // switch (value) {
        //     case "1":
        //         this.menuCountDataSource.data = this.menuCountChartReport.byDay;
        //         break;
        //     case "2":
        //         this.menuCountDataSource.data = this.menuCountChartReport.byWeek;
        //         break;
        //     case "3":
        //         this.menuCountDataSource.data = this.menuCountChartReport.byMonth;
        //         break;
        //     case "4":
        //         this.menuCountDataSource.data = this.menuCountChartReport.byQuarter;
        //         break;
        //     case "5":
        //         this.menuCountDataSource.data = this.menuCountChartReport.byYear;
        //         break;
        // }
        // if (this.menuCountDataSource.data.length == 0) {
        //     this.menuCountDataSource.data.push({
        //         value: 0
        //     });
        // }
    }
    onchageMenuAmountChartReport(value) {
        // switch (value) {
        //     case "1":
        //         this.menuAmountDataSource.data = this.menuAmountChartReport.byDay;
        //         break;
        //     case "2":
        //         this.menuAmountDataSource.data = this.menuAmountChartReport.byWeek;
        //         break;
        //     case "3":
        //         this.menuAmountDataSource.data = this.menuAmountChartReport.byMonth;
        //         break;
        //     case "4":
        //         this.menuAmountDataSource.data = this.menuAmountChartReport.byQuarter;
        //         break;
        //     case "5":
        //         this.menuAmountDataSource.data = this.menuAmountChartReport.byYear;
        //         break;
        // }
        // if (this.menuAmountDataSource.data.length == 0) {
        //     this.menuAmountDataSource.data.push({
        //         value: 0
        //     });
        // }
    }
    
}
