import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { GrowingMethod } from "../models/growing-method.model";


@Injectable({
  providedIn: 'root'
})
export class GrowingMethodService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getList(): Promise<ResultModel<GrowingMethod[]>> {
    return this.http.get<ResultModel<GrowingMethod[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGrowingMethod}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getById(id: any): Promise<ResultModel<GrowingMethod>> {
    return this.http.get<ResultModel<GrowingMethod>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGrowingMethodById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(growingMethod: GrowingMethod): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGrowingMethod}/insert`, { Model: growingMethod }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(growingMethod: GrowingMethod): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGrowingMethod}/update`, { Model: growingMethod }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
