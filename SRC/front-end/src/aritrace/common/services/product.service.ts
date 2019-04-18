import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Product, ProductMultipleLanguage } from "../models/product.model";


@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getList(): Promise<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProduct}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getById(id: any): Promise<ResultModel<ProductMultipleLanguage>> {
    return this.http.get<ResultModel<ProductMultipleLanguage>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProductById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(product: ProductMultipleLanguage): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProduct}/insert`, { Model: product }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(product: ProductMultipleLanguage): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProduct}/update`, { Model: product }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
