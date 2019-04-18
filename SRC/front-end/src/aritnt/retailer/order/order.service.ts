import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from '../common/services/product.service';
import { UoM } from '../common/services/uom.service';

export class Order {
  id: number = 0;
  name: string = '';
  retailerId: number = 0;
  retailerBuyingCalendarId: number = null;
  statusId: number = 0;
  buyingDate: Date = new Date(Date.now());
  billTo: number = 0;
  shipTo: number = 0;
  totalAmount: number = 0;

  //mapping
  items: OrderItem[] = [];

  audits: OrderAudit[] = null;
  orderedDate: Date = null;
  canceledDate: Date = null;
}

export class OrderItem {
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
}

export class OrderStatus {
  id: number = 0;
  name: string = '';
  description: string = '';
}

export class OrderAudit {
  id: number = 0;
  retailerId: number = 0;
  retailerOrderId: number = 0;
  retailerOrderItemId: number = 0;
  statusId: number = 0;
  createdDate: Date = null;
  createdBy: number = 0;

  By: any;
}


@Injectable()
export class OrderService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlUpcompletedOrders: string = '/api/RetailerOrder/gets/un-completed/byuser';
  private urlCompletedOrders: string = '/api/RetailerOrder/gets/completed/byuser';
  private urlOrder: string = '/api/RetailerOrder/get';
  private urlOrderAudit: string = '/api/RetailerOrder/gets/audit';
  private urlAddOrder: string = '/api/RetailerOrder/add';
  private urlUpdateOrder: string = '/api/RetailerOrder/update';
  private urlDeleteOrder: string = '/api/RetailerOrder/delete';
  private urlUpdateStatus: string = '/api/RetailerOrder/update/status';

  private urlStatuses: string = '/api/RetailerOrder/gets/status';

  getsUncompleted(): Observable<ResultModel<Order[]>> {
    return this.http.get<ResultModel<Order[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpcompletedOrders}`, this.authenticSvc.getHttpHeader());
  }

  getsCompleted(from: Date,to: Date): Observable<ResultModel<Order[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    return this.http.get<ResultModel<Order[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCompletedOrders}` + param, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Order>> {
    return this.http.get<ResultModel<Order>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrder}?orderId=${id}`, this.authenticSvc.getHttpHeader());
  }

  getAudit(orderId: number, isHavedOrder: boolean = false) : Observable<ResultModel<OrderAudit[]>> {
    let param:string = `?orderId=${orderId}&isHaveOrder=${isHavedOrder}`;
    return this.http.get<ResultModel<OrderAudit[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrderAudit}${param}`, this.authenticSvc.getHttpHeader());
  }

  update(order: Order) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateOrder}`, { order }, this.authenticSvc.getHttpHeader());
  }
  add(order: Order) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddOrder}`, { order }, this.authenticSvc.getHttpHeader());
  }

  delete(orderId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteOrder}`, { orderId }, this.authenticSvc.getHttpHeader());
  }

  updateStatus(orderId: number, statusId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateStatus}`, { orderId, statusId }, this.authenticSvc.getHttpHeader());
  }

  getStatuses() : Observable<ResultModel<OrderStatus[]>>{
    return this.http.get<ResultModel<OrderStatus[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlStatuses}`, this.authenticSvc.getHttpHeader());
  }

  getCreateTypes() {
    return [
      {
        id: 0,
        name: 'Tạo mới đơn hàng'
      },
      {
        id: 1,
        name: 'Chọn từ kế hoạch mua hàng'
      }
    ];
  }
}
