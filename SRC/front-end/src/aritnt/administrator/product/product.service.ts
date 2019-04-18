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
  categoryId: number = null;

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
  weight: number = 0;
  capacity: number = 0;
  effectivedDate: Date = new Date(Date.now());
}


@Injectable()
export class ProductService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlProducts: string = '/api/Product/gets';
  private urlProductsOnly: string = '/api/Product/gets/only';
  private urlProductsOnlyWithLang: string = '/api/Product/gets/only/withlang';
  private urlProductsForOrder: string = '/api/Product/gets/fororder';
  private urlProductFull: string = '/api/Product/get';
  private urlProductsFull: string = '/api/Product/gets/full';
  private urlProduct: string = '/api/Product/get/full';
  private urlAddProduct: string = '/api/Product/add';
  private urlUpdateProduct: string = '/api/Product/update';
  private urlDeleteProduct: string = '/api/Product/delete';

  getsOnly(): Observable<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProductsOnly}`, this.authenticSvc.getHttpHeader());
  }

  getsForOrder(): Observable<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProductsForOrder}`, this.authenticSvc.getHttpHeader());
  }

  getsOnlyWithLang(): Observable<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProductsOnlyWithLang}`, this.authenticSvc.getHttpHeader());
  }

  gets(): Observable<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProducts}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Product>> {
    return this.http.get<ResultModel<Product>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProduct}?productId=${id}`, this.authenticSvc.getHttpHeader());
  }

  getsFull(): Observable<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProductsFull}`, this.authenticSvc.getHttpHeader());
  }
  getFull(id: number): Observable<ResultModel<Product>> {
    return this.http.get<ResultModel<Product>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProductFull}?productId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(product: Product) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateProduct}`, { product }, this.authenticSvc.getHttpHeader());
  }
  add(product: Product) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddProduct}`, { product }, this.authenticSvc.getHttpHeader());
  }

  delete(productId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteProduct}`, { productId }, this.authenticSvc.getHttpHeader());
  }
}
