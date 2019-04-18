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
export class PartnerService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getByUserId(id: any): Promise<ResultModel<CompanyViewModel>> {
    return this.http.get<ResultModel<CompanyViewModel>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompanyByUserId}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
