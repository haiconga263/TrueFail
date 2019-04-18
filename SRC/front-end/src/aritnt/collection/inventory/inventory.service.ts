import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from 'src/core/models/http.model';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';

export class Inventory {
  id: number = 0;
  traceCode: string = '';
  collectionId: number;
  productId: number;
  uoMId: number;
  quantity: number;

  //mapping data:
  releaseQuantity: number = 0;
  releaseReason: string;
}

export class InventoryHistory {
  id: number = 0;
  traceCode: string = '';
  direction: number;
  collectionId: number;
  productId: number;
  uoMId: number;
  quantity: number;
  lastQuantity: number;
  reason: string;
}

@Injectable()
export class InventoryService {
    constructor(private http: HttpClient, private authenticSvc: AuthenticService) {
    }

    private urlInventoriesBySKU = '/api/CollectionInventory/gets/by-sku';
    private urlInventories = '/api/CollectionInventory/gets';

    private urlUpdateInventories = '/api/CollectionInventory/update';
  
    public getBySKUs(collectionId: number): Observable<ResultModel<Inventory[]>> {
      return this.http.get<ResultModel<Inventory[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlInventoriesBySKU}?collectionId=${collectionId}`, this.authenticSvc.getHttpHeader());
    }

    public gets(collectionId: number): Observable<ResultModel<Inventory[]>> {
      return this.http.get<ResultModel<Inventory[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlInventories}?collectionId=${collectionId}`, this.authenticSvc.getHttpHeader());
    }

    public releaseCorruptedGoods(collectionId: number, traceCode: string, quantity: number, reason: string): Observable<ResultModel<any>> {
      return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateInventories}`, {
        direction: 0,
        collectionId: collectionId,
        traceCode,
        quantity,
        reason
      }, this.authenticSvc.getHttpHeader());
    }
}
