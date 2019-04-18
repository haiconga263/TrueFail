import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppConsts } from "src/core/constant/AppConsts";
import { FarmerOrder } from "../receiving/receiving.service";
import { InventoryHistory } from "../inventory/inventory.service";
import { CFShipping } from "../shipping/shipping.service";

export class ReportByEmployee {
  employeeId: number = 0;
  totalOrder: number = 0;
  totalAmount: number = 0;
  canceledCount: number = 0;
  completedCount: number = 0;
}

@Injectable()
export class ReportService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlOrderHistories = '/api/CollectionReport/gets/completed';
  private urlOrderHistoriesByEmployee = '/api/CollectionReport/gets/completed/by-employee';
  private urlInventoryHistories = '/api/CollectionReport/gets/inventory';
  private urlShippingHistories = '/api/CollectionReport/gets/shipping';

  public getHistories(from: Date, to: Date, collectionId: number): Observable<ResultModel<FarmerOrder[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    param += "&collectionId=" + collectionId.toString();
    return this.http.get<ResultModel<FarmerOrder[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrderHistories}` + param, this.authenticSvc.getHttpHeader());
  }

  public getHistoriesByEmployee(from: Date, to: Date, collectionId: number): Observable<ResultModel<ReportByEmployee[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    param += "&collectionId=" + collectionId.toString();
    return this.http.get<ResultModel<ReportByEmployee[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrderHistoriesByEmployee}` + param, this.authenticSvc.getHttpHeader());
  }

  public getInventoryHistories(from: Date, to: Date, collectionId: number): Observable<ResultModel<InventoryHistory[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    param += "&collectionId=" + collectionId.toString();
    return this.http.get<ResultModel<InventoryHistory[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlInventoryHistories}` + param, this.authenticSvc.getHttpHeader());
  }

  public getShippingHistories(from: Date, to: Date, collectionId: number): Observable<ResultModel<CFShipping[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    param += "&collectionId=" + collectionId.toString();
    return this.http.get<ResultModel<CFShipping[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlShippingHistories}` + param, this.authenticSvc.getHttpHeader());
  }
}
