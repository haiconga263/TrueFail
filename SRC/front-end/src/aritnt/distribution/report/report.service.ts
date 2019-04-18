import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { Trip, Order } from "../trip/trip.service";
import { AppConsts } from "src/core/constant/AppConsts";


@Injectable()
export class ReportService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlTripHistorys = '/api/trip/gets/history';
  private urlOrderHistorys = '/api/trip/gets/order/history';

  public getHistorys(distributionId: number, from: Date, to: Date): Observable<ResultModel<Trip[]>> {
    let param = '';
    param += "?distributionId=" + distributionId.toString();
    param += "&from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    return this.http.get<ResultModel<Trip[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlTripHistorys}` + param, this.authenticSvc.getHttpHeader());
  }

  public getOrderHistorys(distributionId: number, from: Date, to: Date): Observable<ResultModel<Order[]>> {
    let param = '';
    param += "?distributionId=" + distributionId.toString();
    param += "&from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    return this.http.get<ResultModel<Order[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrderHistorys}` + param, this.authenticSvc.getHttpHeader());
  }
}
