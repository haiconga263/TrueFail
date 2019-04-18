import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Process, ProcessInformation } from "../models/production-process.model";


@Injectable({
  providedIn: 'root'
})
export class ProcessService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getById(id: any): Promise<ResultModel<ProcessInformation>> {
    return this.http.post<ResultModel<ProcessInformation>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProcessById}`, { Model: id }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  delete(id: any): Promise<ResultModel<ProcessInformation>> {
    return this.http.post<ResultModel<ProcessInformation>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProcess}/delete`, { Model: id }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getNew(): Promise<ResultModel<ProcessInformation>> {
    return this.http.post<ResultModel<ProcessInformation>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProcess}/new`, {}, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(process: ProcessInformation): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProcess}/update`, { Model: process }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
