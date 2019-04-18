import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from '../../product/product.service';
import { UoM } from './uom.service';
import { Farmer } from '../../farmer/farmer.service';

export class VirtualIntProduct {
    productId: number;
    product: Product;
    farmerProducts: FarmerProduct[] = [];
}

export class FarmerProduct {
  farmerId: number = 0;
  productId: number = 0;
  uoMId: number = 0;
  quantity: number = 0;
  effectivedDate: Date

  //mapping
  farmer: Farmer;
  product: Product;
  uoM: UoM;
}


@Injectable()
export class InventoryService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlVirtuals: string = '/api/VirtualInventory/get/by-product';

  get(productId: number, effect: Date): Observable<ResultModel<FarmerProduct[]>> {
    return this.http.get<ResultModel<FarmerProduct[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlVirtuals}?productId=${productId}&effect=${effect}`, this.authenticSvc.getHttpHeader());
  }
}