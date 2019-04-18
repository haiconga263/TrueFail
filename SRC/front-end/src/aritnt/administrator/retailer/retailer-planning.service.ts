import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from '../product/product.service';
import { UoM } from '../common/services/uom.service';

export class RetailerPlanning {
  id: number = 0;
  code: string = '';
  name: string = '';
  retailerId: number = 0;
  buyingDate: Date = new Date(Date.now());
  isOrdered: boolean = false;
  isExpired: boolean = false;
  isAdaped: boolean = false;
  AdapNote: string = '';
  isUsed: boolean = true;

  //mapping
  items: RetailerPlanningItem[] = [];
}

export class RetailerPlanningItem {
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
export class RetailerPlanningService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlUpcompletedPlannings: string = '/api/RetailerBuyingCalendar/gets/un-completed';
  private urlCompletedPlannings: string = '/api/RetailerBuyingCalendar/gets/completed';
  private urlPlanning: string = '/api/RetailerBuyingCalendar/get';
  private urlAddPlanning: string = '/api/RetailerBuyingCalendar/add';
  private urlUpdatePlanning: string = '/api/RetailerBuyingCalendar/update';
  private urlDeletePlanning: string = '/api/RetailerBuyingCalendar/delete';
  private urlAdapPlanning: string = '/api/RetailerBuyingCalendar/update/adap';

  getsUncompleted(retailerId: number = 0): Observable<ResultModel<RetailerPlanning[]>> {
    let param = '';
    if(retailerId > 0)
    {
      param += '?retailerId = ' + retailerId;
    }
    return this.http.get<ResultModel<RetailerPlanning[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpcompletedPlannings}` + param, this.authenticSvc.getHttpHeader());
  }

  getsCompleted(from: Date,to: Date, retailerId: number = 0): Observable<ResultModel<RetailerPlanning[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    if(retailerId > 0)
    {
      param += '&retailerId = ' + retailerId;
    }
    return this.http.get<ResultModel<RetailerPlanning[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCompletedPlannings}` + param, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<RetailerPlanning>> {
    return this.http.get<ResultModel<RetailerPlanning>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPlanning}?buyingCalendarId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(buyingCalendar: RetailerPlanning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatePlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }
  add(buyingCalendar: RetailerPlanning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddPlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }

  delete(duyingCalendarId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletePlanning}`, { duyingCalendarId }, this.authenticSvc.getHttpHeader());
  }

  updateAdap(buyingCalendar: RetailerPlanning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAdapPlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }
}
