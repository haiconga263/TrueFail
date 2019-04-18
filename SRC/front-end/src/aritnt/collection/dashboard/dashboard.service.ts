import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable()
export class DashBoardReportService {
    constructor(private http: HttpClient) {
    }

    // async salesOrderReport(): Promise<APIResult> {
    //     let response = await functions.httpGet(this.http, environment.apiHost + '/api/dashboard/ReportSalesOrder', null);
    //     return response;
    // }
    // async salesOrderCancelReport(): Promise<APIResult> {
    //     let response = await functions.httpGet(this.http, environment.apiHost + '/api/dashboard/ReportSalesOrderCancel', null);
    //     return response;
    // }
    // async salesOrderInprogressReport(): Promise<APIResult> {
    //     let response = await functions.httpGet(this.http, environment.apiHost + '/api/dashboard/ReportSalesOrderInprogress', null);
    //     return response;
    // }
    // async salesOrderAmountReport(): Promise<APIResult> {
    //     let response = await functions.httpGet(this.http, environment.apiHost + '/api/dashboard/ReportSalesOrderAmount', null);
    //     return response;
    // }

    // async buyOrderAmountReport(): Promise<APIResult> {
    //     let response = await functions.httpGet(this.http, environment.apiHost + '/api/dashboard/ReportBuyOrderAmount', null);
    //     return response;
    // }

    // async MenuCountChartReport(): Promise<APIResult> {
    //     let response = await functions.httpGet(this.http, environment.apiHost + '/api/dashboard/ReportChartMenuCount', null);
    //     return response;
    // }

    // async MenuAmountChartReport(): Promise<APIResult> {
    //     let response = await functions.httpGet(this.http, environment.apiHost + '/api/dashboard/ReportChartMenuAmount', null);
    //     return response;
    // }

    getTimeType() : any[] {
        return [
            {
                id: "1",
                name: "Ngày"
            },
            {
                id: "2",
                name: "Tuần"
            },
            {
                id: "3",
                name: "Tháng"
            },
            {
                id: "4",
                name: "Quý"
            },
            {
                id: "5",
                name: "Năm"
            },
        ];
    }
}
