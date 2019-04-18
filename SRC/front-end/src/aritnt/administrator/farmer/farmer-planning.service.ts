import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from '../product/product.service';
import { UoM } from '../common/services/uom.service';

export class FarmerPlanning {
  id: number = 0;
  code: string = '';
  name: string = '';
  farmerId: number = 0;
  buyingDate: Date = new Date(Date.now());
  isOrdered: boolean = false;
  isExpired: boolean = false;
  isAdaped: boolean = false;
  AdapNote: string = '';
  isUsed: boolean = true;

  //mapping
  items: FarmerPlanningItem[] = [];
}

export class FarmerPlanningItem {
  id: number = 0;
  farmerBuyingCalendarId: number = 0;
  productId: number = 0;
  quantity: number = 0;
  adapQuantity: number = 0;
  uoMId: number = 0;

  //mapping
  product: Product = new Product();
  uom: UoM = new UoM();
}

export class TempFarmerPlanningItem {
  id: number = 0;
  farmerId: number = 0;
  quantity: number = 0;
  productId: number = 0;
  uoMId: number = 0;

   // Key in devextreme datagrid
  __KEY__: string = '';
}


@Injectable()
export class FarmerPlanningService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlUpcompletedPlannings: string = '/api/FarmerBuyingCalendar/gets/un-completed';
  private urlCompletedPlannings: string = '/api/FarmerBuyingCalendar/gets/completed';
  private urlPlanning: string = '/api/FarmerBuyingCalendar/get';
  private urlAddPlanning: string = '/api/FarmerBuyingCalendar/add';
  private urlUpdatePlanning: string = '/api/FarmerBuyingCalendar/update';
  private urlDeletePlanning: string = '/api/FarmerBuyingCalendar/delete';
  private urlAdapPlanning: string = '/api/FarmerBuyingCalendar/update/adap';

  getsUncompleted(rarmerId: number = 0): Observable<ResultModel<FarmerPlanning[]>> {
    let param = '';
    if(rarmerId > 0)
    {
      param += '?rarmerId = ' + rarmerId;
    }
    return this.http.get<ResultModel<FarmerPlanning[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpcompletedPlannings}` + param, this.authenticSvc.getHttpHeader());
  }

  getsCompleted(from: Date,to: Date, rarmerId: number = 0): Observable<ResultModel<FarmerPlanning[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    if(rarmerId > 0)
    {
      param += '&rarmerId = ' + rarmerId;
    }
    return this.http.get<ResultModel<FarmerPlanning[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCompletedPlannings}` + param, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<FarmerPlanning>> {
    return this.http.get<ResultModel<FarmerPlanning>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPlanning}?buyingCalendarId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(buyingCalendar: FarmerPlanning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatePlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }
  add(buyingCalendar: FarmerPlanning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddPlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }

  delete(duyingCalendarId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletePlanning}`, { duyingCalendarId }, this.authenticSvc.getHttpHeader());
  }

  updateAdap(buyingCalendar: FarmerPlanning) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAdapPlanning}`, { buyingCalendar }, this.authenticSvc.getHttpHeader());
  }
}
