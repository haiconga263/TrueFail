import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from '../common/services/geo.service';
import { Product } from '../common/services/product.service';
import { UoM } from '../common/services/uom.service';

export class Collection {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  managerId: number = 0;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';
  isUsed: boolean = true;

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();

  items: FarmerOrder[] = [];
}

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
  items: FarmerOrderItem[] = [];
  currentTotalAmount: number = 0;
}

export class FarmerOrderItem {
  id: number = 0;
  farmerOrderId: number = 0;
  productId: number = 0;
  statusId: number = 0;
  price: number = 0;
  orderedQuantity: number = 0;
  deliveriedQuantity: number = 0;
  uoMId: number = 0;

  //mapping
  product: Product = new Product();
  uom: UoM = new UoM();
  totalAmount: number = 0;
}

export class FarmerOrderStatus {
  id: number = 0;
  name: string = '';
  description: string = '';
}

@Injectable()
export class ReceivingService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlCollections: string = '/api/collection/gets/by-owner';
  private urlCollectionsByManager: string = '/api/collection/gets/by-owner-manager';
  private urlCollectionOrders: string = '/api/collection/gets/orders';

  private urlUpdateInCollectionOrders: string = '/api/FarmerOrder/update/in-collector';
  private urlStatuses: string = '/api/FarmerOrder/gets/status';

  gets(): Observable<ResultModel<Collection[]>> {
    return this.http.get<ResultModel<Collection[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCollections}`, this.authenticSvc.getHttpHeader());
  }

  getsByManager(): Observable<ResultModel<Collection[]>> {
    return this.http.get<ResultModel<Collection[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCollectionsByManager}`, this.authenticSvc.getHttpHeader());
  }

  getsOrder(collectionId: number): Observable<ResultModel<FarmerOrder[]>> {
    return this.http.get<ResultModel<FarmerOrder[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCollectionOrders}?collectionId=${collectionId}`, this.authenticSvc.getHttpHeader());
  }

  getStatuses() : Observable<ResultModel<FarmerOrderStatus[]>>{
    return this.http.get<ResultModel<FarmerOrderStatus[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlStatuses}`, this.authenticSvc.getHttpHeader());
  }

  complete(order: FarmerOrder) : Observable<ResultModel<number>> {
    order.statusId = FarmerOrderStatuses.Completed;
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateInCollectionOrders}`, { order }, this.authenticSvc.getHttpHeader());
  }

  cancel(order: FarmerOrder) : Observable<ResultModel<number>> {
    order.statusId = FarmerOrderStatuses.Canceled;
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateInCollectionOrders}`, { order }, this.authenticSvc.getHttpHeader());
  }
}
