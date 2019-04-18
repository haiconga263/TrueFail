import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Product {
  id: number = 0;
  code: string = '';
  imageURL: string = '';
  defaultName: string = '';
  defaultDescription: string = '';
  defaultBuyingPrice: number = 0;
  defaultSellingPrice: number = 0;
  isUsed: boolean = true;
  languages : ProductLanguage[] = [];
  prices: ProductPrice[] = [];

  //mapping
  imageData: string = '';
}

export class ProductLanguage {
  id: number = 0;
  productId: number = 0;
  languageId: number = 0;
  name: string = '';
  description: string = '';
}

export class ProductPrice {
  id: number = 0;
  productId: number = 0;
  uoMId: number = 0;
  buyingPrice: number = 0;
  sellingPrice: number = 0;
  effectivedDate: Date = new Date(Date.now());
}


@Injectable()
export class PriceService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlProducts: string = '/api/Product/gets';

  gets(): Observable<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProducts}`, this.authenticSvc.getHttpHeader());
  }
}
