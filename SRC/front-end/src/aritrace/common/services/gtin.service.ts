import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { FuncHelper } from "src/core/helpers/function-helper";
import { GTIN, GTINInformation, GTINTypes } from "../models/gtin.model";

@Injectable({
  providedIn: 'root'
})
export class GTINService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService) {
  }

  public generateGTIN(type: GTINTypes): Promise<ResultModel<GTINInformation>> {
    return this.http.post<ResultModel<GTINInformation>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGTIN}/generate`, { Model: type }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  public checkNewGTIN(gtin: GTINInformation): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGTIN}/checknew`, { Model: gtin }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  public calculateCheckDigitByGTIN(gtin: GTINInformation): Promise<ResultModel<GTINInformation>> {
    return this.http.post<ResultModel<GTINInformation>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGTIN}/calculatecheckdigit`, { Model: gtin }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  public getGTINTypes(): any[] {
    return [
      { text: 'GTIN-8', value: GTINTypes.gtin_8 },
      { text: 'GTIN-12', value: GTINTypes.gtin_12 },
      { text: 'GTIN-13', value: GTINTypes.gtin_13 },
      { text: 'GTIN-14', value: GTINTypes.gtin_14 },
    ]
  }
}
