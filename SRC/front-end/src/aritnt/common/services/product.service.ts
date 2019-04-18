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

    public constructor(init?: Partial<Product>) {
      Object.assign(this, init);
    }
  }
  
  export class ProductLanguage {
    id: number = 0;
    productId: number = 0;
    languageId: number = 0;
    name: string = '';
    description: string = '';

    public constructor(init?: Partial<ProductLanguage>) {
      Object.assign(this, init);
    }
  }
  
  export class ProductPrice {
    id: number = 0;
    productId: number = 0;
    uoMId: number = 0;
    buyingPrice: number = 0;
    sellingPrice: number = 0;
    effectivedDate: Date = new Date(Date.now());

    public constructor(init?: Partial<ProductPrice>) {
      Object.assign(this, init);
    }
  }

  @Injectable()
  export class ProductService {
    constructor(
      private http: HttpClient,
      private authenticSvc: AuthenticService) { }
  
    private urlProductsOnly: string = '/api/Product/gets/only';
  
    getsOnly(): Observable<ResultModel<Product[]>> {
      return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProductsOnly}`, this.authenticSvc.getHttpHeader());
    }
  }
  