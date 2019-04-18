import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { CultureField } from "../models/culture-field.model";


@Injectable({
  providedIn: 'root'
})
export class CultureFieldService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getList(): Promise<ResultModel<CultureField[]>> {
    return this.http.get<ResultModel<CultureField[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCultureField}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getById(id: any): Promise<ResultModel<CultureField>> {
    return this.http.get<ResultModel<CultureField>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCultureFieldById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(cultureField: CultureField): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCultureField}/insert`, { Model: cultureField }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(cultureField: CultureField): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCultureField}/update`, { Model: cultureField }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
