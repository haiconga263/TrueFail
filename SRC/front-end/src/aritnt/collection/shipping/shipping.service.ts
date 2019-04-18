import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class CFShipping {
  id: number = 0;
  code: string = '';
  collectionId: number = null;
  fulfillmentId: number = null;
  statusId: number = null;
  shipperId: number = null;
  vehicleId: number = null;
  deliveryDate: Date;

  items: CFShippingItem[] = null;
}

export class CFShippingItem {
  id: number = 0;
  traceCode: string = '';
  productId: number;
  uoMId: string;
  shippedQuantity: number;
  deliveriedQuantity: number;

  //mapping data
  __KEY__: string;
}

@Injectable()
export class ShippingService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlShippings: string = '/api/CollectionShipping/gets/un-completed';
  private urlShippingItems: string = '/api/CollectionShipping/gets/item';

  private urlAddShipping: string = '/api/CollectionShipping/add';
  private urlUpdateShipping: string = '/api/CollectionShipping/update';
  private urlDeleteShipping: string = '/api/CollectionShipping/delete';

  private urlUpdateItems: string = '/api/CollectionShipping/update/items';

  private urlUpdateShippingStatus: string = '/api/CollectionShipping/update/status';

  gets(collectionId: number): Observable<ResultModel<CFShipping[]>> {
    return this.http.get<ResultModel<CFShipping[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlShippings}?collectionId=${collectionId}`, this.authenticSvc.getHttpHeader());
  }

  getItems(shippingId: number): Observable<ResultModel<CFShippingItem[]>> {
    return this.http.get<ResultModel<CFShippingItem[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlShippingItems}?shippingId=${shippingId}`, this.authenticSvc.getHttpHeader());
  }


  changeStatus(shippingId: number, statusId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateShippingStatus}`, {shippingId, statusId}, this.authenticSvc.getHttpHeader());
  }

  add(shipping: CFShipping) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddShipping}`, { shipping }, this.authenticSvc.getHttpHeader());
  }

  update(shipping: CFShipping) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateShipping}`, { shipping }, this.authenticSvc.getHttpHeader());
  }

  delete(shippingId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteShipping}`, { shippingId }, this.authenticSvc.getHttpHeader());
  }

  updateItems(shippingId: number, items: CFShippingItem[]) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateItems}`, { shippingId, items }, this.authenticSvc.getHttpHeader());
  }

  getStatuses() : any[] {
    return [
      {
        id: 1,
        name: "Created",
        flagColor: "#D7D7D7"
      },
      {
        id: 2,
        name: "OnConfirmed",
        flagColor: "#0000FF"
      },
      {
        id: 3,
        name: "OnTriped",
        flagColor: "#00FFFF"
      },
      {
        id: 4,
        name: "Finished",
        flagColor: "#00FF00"
      },
      {
        id: -1,
        name: "Canceled",
        flagColor: "#FF0000"
      },
    ];
  }
}
