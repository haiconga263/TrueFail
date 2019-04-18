import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from '../product/product.service';
import { UoM } from '../common/services/uom.service';
import { TempFarmerPlanningItem } from '../farmer/farmer-planning.service';

export class RetailerOrder {
  id: number = 0;
  name: string = '';
  retailerId: number = 0;
  retailerBuyingCalanderId: number = 0;
  statusId: number = 0;
  buyingDate: Date = new Date(Date.now());
  billTo: number = 0;
  shipTo: number = 0;
  totalAmount: number = 0;

  //mapping
  items: RetailerOrderItem[] = [];
}

export class RetailerOrderItem {
  id: number = 0;
  retailerOrderId: number = 0;
  productId: number = 0;
  statusId: number = 0;
  price: number = 0;
  orderedQuantity: number = 0;
  adapQuantity: number = 0;
  deliveriedQuantity: number = 0;
  uoMId: number = 0;

  //mapping
  product: Product = new Product();
  uom: UoM = new UoM();
  totalAmount: number = 0;

  __KEY__: string = '';
  //retailer order page variable
  isAdaped: boolean = false;
  planningItems: TempFarmerPlanningItem[] = [];
}

export class RetailerOrderStatus {
  id: number = 0;
  name: string = '';
  description: string = '';
}

export class RetailerOrderProcessing {
  orderId: number = 0;
  items: RetailerOrderItemProcessing[] = [];
}

export class RetailerOrderItemProcessing {
  orderItemId: number = 0;
  plannings: RetailerOrderPlanningItemProcessing[] = [];
}
export class RetailerOrderPlanningItemProcessing {
  farmerId: number = 0;
  quantity: number = 0;
}


@Injectable()
export class RetailerOrderService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlUpcompletedOrders: string = '/api/RetailerOrder/gets/un-completed';
  private urlCompletedOrders: string = '/api/RetailerOrder/gets/completed';
  private urlOrder: string = '/api/RetailerOrder/get';
  private urlAddOrder: string = '/api/RetailerOrder/add';
  private urlUpdateOrder: string = '/api/RetailerOrder/update';
  private urlDeleteOrder: string = '/api/RetailerOrder/delete';
  private urlUpdateStatus: string = '/api/RetailerOrder/update/status';
  private urlProcess: string = '/api/RetailerOrder/proccessing';

  private urlStatuses: string = '/api/RetailerOrder/gets/status';

  getsUncompleted(retailerId: number = 0): Observable<ResultModel<RetailerOrder[]>> {
    let param = '';
    if(retailerId > 0)
    {
      param += '?retailerId = ' + retailerId;
    }
    return this.http.get<ResultModel<RetailerOrder[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpcompletedOrders}` + param, this.authenticSvc.getHttpHeader());
  }

  getsCompleted(from: Date,to: Date, retailerId: number = 0): Observable<ResultModel<RetailerOrder[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    if(retailerId > 0)
    {
      param += '&retailerId = ' + retailerId;
    }
    return this.http.get<ResultModel<RetailerOrder[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCompletedOrders}` + param, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<RetailerOrder>> {
    return this.http.get<ResultModel<RetailerOrder>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrder}?orderId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(order: RetailerOrder) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateOrder}`, { order }, this.authenticSvc.getHttpHeader());
  }
  add(order: RetailerOrder) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddOrder}`, { order }, this.authenticSvc.getHttpHeader());
  }

  delete(orderId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteOrder}`, { orderId }, this.authenticSvc.getHttpHeader());
  }

  updateStatus(orderId: number, statusId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateStatus}`, { orderId, statusId }, this.authenticSvc.getHttpHeader());
  }

  process(processing: RetailerOrderProcessing) : Observable<ResultModel<any[]>>{
    return this.http.post<ResultModel<any[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProcess}`, { processing } , this.authenticSvc.getHttpHeader());
  }

  getStatuses() : Observable<ResultModel<RetailerOrderStatus[]>>{
    return this.http.get<ResultModel<RetailerOrderStatus[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlStatuses}`, this.authenticSvc.getHttpHeader());
  }
}
