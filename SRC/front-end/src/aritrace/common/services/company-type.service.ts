import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { CompanyType } from "../models/company-type.model";


@Injectable({
  providedIn: 'root'
})
export class CompanyTypeService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getById(id: any): Promise<ResultModel<CompanyType>> {
    return this.http.get<ResultModel<CompanyType>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompanyTypeById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(companyType: CompanyType): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompanyType}/insert`, { Model: companyType }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(companyType: CompanyType): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompanyType}/update`, { Model: companyType }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
