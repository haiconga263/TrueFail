import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Caption, CaptionMultipleLanguage } from "../models/caption.model";


@Injectable({
  providedIn: 'root'
})
export class CaptionService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getById(id: any): Promise<ResultModel<CaptionMultipleLanguage>> {
    return this.http.get<ResultModel<CaptionMultipleLanguage>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCaptionById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(caption: CaptionMultipleLanguage): Promise<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCaption}/insert`, { Model: caption }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(caption: CaptionMultipleLanguage): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCaption}/update`, { Model: caption }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
