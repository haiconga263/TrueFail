import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from '../product/product.service';
import { UoM } from '../common/services/uom.service';
import { Farmer } from './farmer.service';

export enum FarmerOrderStatuses {
  BeginOrder = 1,
  ConfirmedOrder = 2,
  Completed = 3,
  Canceled = -1
}

export class FarmerOrder {
  id: number = 0;
  name: string = '';
  farmerId: number = 0;
  farmerBuyingCalendarId: number = 0;
  statusId: number = 0;
  buyingDate: Date = new Date(Date.now());
  collectionId: number = 0;
  shipTo: number = 0;
  totalAmount: number = 0;

  //mapping
  farmer: Farmer = new Farmer();
  items: FarmerOrderItem[] = [];
}

export class FarmerOrderItem {
  id: number = 0;
  farmerOrderId: number = 0;
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

export class FarmerOrderStatus {
  id: number = 0;
  name: string = '';
  description: string = '';
}


@Injectable()
export class FarmerOrderService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlUpcompletedOrders: string = '/api/FarmerOrder/gets/un-completed';
  private urlCompletedOrders: string = '/api/FarmerOrder/gets/completed';
  private urlOrder: string = '/api/FarmerOrder/get';
  private urlAddOrder: string = '/api/FarmerOrder/add';
  private urlUpdateOrder: string = '/api/FarmerOrder/update';
  private urlDeleteOrder: string = '/api/FarmerOrder/delete';
  private urlUpdateStatus: string = '/api/FarmerOrder/update/status';

  private urlStatuses: string = '/api/FarmerOrder/gets/status';

  getsUncompleted(farmerId: number = 0): Observable<ResultModel<FarmerOrder[]>> {
    let param = '';
    if(farmerId > 0)
    {
      param += '?farmerId = ' + farmerId;
    }
    return this.http.get<ResultModel<FarmerOrder[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpcompletedOrders}` + param, this.authenticSvc.getHttpHeader());
  }

  getsCompleted(from: Date,to: Date, farmerId: number = 0): Observable<ResultModel<FarmerOrder[]>> {
    let param = '';
    param += "?from=" + from.toDateString();
    param += "&to=" + to.toDateString();
    if(farmerId > 0)
    {
      param += '&farmerId = ' + farmerId;
    }
    return this.http.get<ResultModel<FarmerOrder[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCompletedOrders}` + param, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<FarmerOrder>> {
    return this.http.get<ResultModel<FarmerOrder>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrder}?orderId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(order: FarmerOrder) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateOrder}`, { order }, this.authenticSvc.getHttpHeader());
  }
  add(order: FarmerOrder) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddOrder}`, { order }, this.authenticSvc.getHttpHeader());
  }

  delete(orderId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteOrder}`, { orderId }, this.authenticSvc.getHttpHeader());
  }

  updateStatus(orderId: number, statusId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateStatus}`, { orderId, statusId }, this.authenticSvc.getHttpHeader());
  }

  getStatuses() : Observable<ResultModel<FarmerOrderStatus[]>>{
    return this.http.get<ResultModel<FarmerOrderStatus[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlStatuses}`, this.authenticSvc.getHttpHeader());
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
