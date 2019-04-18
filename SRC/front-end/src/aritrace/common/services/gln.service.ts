import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { FuncHelper } from "src/core/helpers/function-helper";
import { GLN, GLNDetailViewModel } from "../models/gln.model";

@Injectable({
  providedIn: 'root'
})
export class GLNService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService) {
  }

  public generateGLN(): Promise<ResultModel<GLNDetailViewModel>> {
    return this.http.post<ResultModel<GLNDetailViewModel>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGLN}/generate`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  public checkNewGLN(gln: GLNDetailViewModel): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGLN}/checknew`, { Model: gln }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  public calculateCheckDigitByGLN(gln: GLNDetailViewModel): Promise<ResultModel<GLNDetailViewModel>> {
    return this.http.post<ResultModel<GLNDetailViewModel>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGLN}/calculatecheckdigit`, { Model: gln }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  //public getGTINTypes(): any[] {
  //  return [
  //    { text: 'GTIN-8', value: GTINTypes.gtin_8 },
  //    { text: 'GTIN-12', value: GTINTypes.gtin_12 },
  //    { text: 'GTIN-13', value: GTINTypes.gtin_13 },
  //    { text: 'GTIN-14', value: GTINTypes.gtin_14 },
  //  ]
  //}
  getById(id: any): Promise<ResultModel<GLN>> {
    return this.http.get<ResultModel<GLN>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGLNById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(glnDetail: GLN): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGLN}/insert`, { Model: glnDetail }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(glnDetail: GLN): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiGLN}/update`, { Model: glnDetail }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}





