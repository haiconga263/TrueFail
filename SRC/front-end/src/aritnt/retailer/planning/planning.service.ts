import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from '../price/price.service';
import { UoM } from '../common/services/uom.service';

export class Planning {
  id: number = 0;
  name: string = '';
  retailerId: number = 0;
  buyingDate: Date = new Date(Date.now());
  isOrdered: boolean = false;
  isExpired: boolean = false;
  isAdaped: boolean = false;
  AdapNote: string = '';
  isUsed: boolean = true;

  //mapping
  items: PlanningItem[] = [];
}

export class PlanningItem {
  id: number = 0;
  retailerBuyingCalendarId: number = 0;
  productId: number = 0;
  quantity: number = 0;
  adapQuantity: number = 0;
  uoMId: number = 0;

  //mapping
  product: Product = new Product();
  uom: UoM = new UoM();
}

@Injectable()
export class PlanningService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlPlannings: string = '/api/RetailerBuyingCalendar/gets/byuser';
  private urlPlanning: string = '/api/RetailerBuyingCalendar/get';
  private urlUpcompletedPlanningsByUser: string = '/api/RetailerBuyingCalendar/gets/un-completed/byuser';
  private urlAddPlanning: string = '/api/RetailerBuyingCalendar/add';
  private urlUpdatePlanning: string = '/api/RetailerBuyingCalendar/update';
  private urlDeletePlanning: string = '/api/RetailerBuyingCalendar/delete';

  gets(from: Date,to: Date, status: number, userId: number): Observable<ResultModel<Planning[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    param += "&status=" + status;
    param += '&userId=' + userId;
    return this.http.get<ResultModel<Planning[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPlannings}` + param, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Planning>> {
    return this.http.get<ResultModel<Planning>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPlanning}?buyingCalendarId=${id}`, this.authenticSvc.getHttpHeader());
  }

  getsUncompletedByUser(): Observable<ResultModel<Planning[]>> {
    return this.http.get<ResultModel<Planning[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpcompletedPlanningsByUser}`, this.authenticSvc.getHttpHeader());
  }

  update(buyingCalendar: Planning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatePlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }
  add(buyingCalendar: Planning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddPlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }

  delete(buyingCalendarId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletePlanning}`, { buyingCalendarId }, this.authenticSvc.getHttpHeader());
  }

  getStatuses(){
    return [
      {
        id: 0,
        name: "Tất cả"
      },
      {
        id: 1,
        name: "Đang xử lý"
      },
      {
        id: 2,
        name: "Đã xử lý"
      }
    ]
  }
}
