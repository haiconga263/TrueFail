import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { ResultModel } from "../models/http.model";
import { ResultCode } from "../constant/AppEnums";
import { FuncHelper } from "../helpers/function-helper";

export enum MethodTypes {
  post,
  get,
  put,
  delete
}

export class HttpConfig {
  public name: string;
  public nameField: string;
  public method: MethodTypes;

  public constructor(init?: Partial<HttpConfig>) {
    Object.assign(this, init);
    this.name = FuncHelper.isNull(this.name) ? 'all' : this.name;
    this.nameField = FuncHelper.isNull(this.nameField) ? 'model' : this.nameField;
    this.method = FuncHelper.isNull(this.method) ? MethodTypes.post : this.method;
  }
}

@Injectable({
  providedIn: 'root'
})
export class ImplementorService {
  public UrlApi: string = '';

  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  sendRequest(config: HttpConfig, data: any = {}): any {
    let httpOptions = this.authenticSvc.getHttpHeader();
    let url = `${AppConsts.remoteServiceBaseUrl}${this.UrlApi}`;
    let result;
    let param = {};

    param[config.nameField] = data;

    if (config == null)
      config = new HttpConfig();

    if (config.method == MethodTypes.post)
      result = this.http.post(`${url}/${config.name}`, param, httpOptions);
    else result = this.http.get(`${url}/${config.name}?${FuncHelper.ConvertToParamString(data)}`, httpOptions);

    return result
      .toPromise()
      .then((data: ResultModel<any>) => {
        return data;
      })
      .catch(e => {
        return new ResultModel<any>({
          data: e.error,
          result: ResultCode.FailMsg,
          errorMessage: FuncHelper.isNull(e.error) ? '' : e.error.Message
        });
      });
  }
}
