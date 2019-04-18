import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Production, ProductionInformation } from "../models/production.model";


@Injectable({
  providedIn: 'root'
})
export class ProductionService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getList(): Promise<ResultModel<ProductionInformation[]>> {
    return this.http.get<ResultModel<ProductionInformation[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProduction}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getById(id: any): Promise<ResultModel<ProductionInformation>> {
    return this.http.post<ResultModel<ProductionInformation>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProductionById}`, { Model: id }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(product: ProductionInformation): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProduction}/insert`, { Model: product }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(product: ProductionInformation): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProduction}/update`, { Model: product }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
