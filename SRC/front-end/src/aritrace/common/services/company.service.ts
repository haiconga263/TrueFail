import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Company, CompanyViewModel } from "../models/company.model";


@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getList(): Promise<ResultModel<CompanyViewModel[]>> {
    return this.http.get<ResultModel<CompanyViewModel[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompany}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getById(id: any): Promise<ResultModel<CompanyViewModel>> {
    return this.http.get<ResultModel<CompanyViewModel>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompanyById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(company: CompanyViewModel): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompany}/insert`, { Model: company }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(company: CompanyViewModel): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompany}/update`, { Model: company }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
