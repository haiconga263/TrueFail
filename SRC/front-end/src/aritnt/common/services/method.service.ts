import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export class Method {
  id: number = 0;
  code: string = '';
  name: string = '';
  isUsed: boolean = true;

  public constructor(init?: Partial<Method>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class MethodService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlMethodCategories: string = `${appUrl.apiMethod}/gets`;
  private urlAllMethodCategories: string = `${appUrl.apiMethod}/gets/all`;
  private urlmethodById: string = `${appUrl.apiMethod}/get`;
  private urlAddmethod: string = `${appUrl.apiMethod}/insert`;
  private urlUpdatemethod: string = `${appUrl.apiMethod}/update`;
  private urlDeletemethod: string = `${appUrl.apiMethod}/delete`;

  gets(): Observable<ResultModel<Method[]>> {
    return this.http.get<ResultModel<Method[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlMethodCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<Method[]>> {
    return this.http.get<ResultModel<Method[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllMethodCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Method>> {
    return this.http.get<ResultModel<Method>>(`${AppConsts.remoteServiceBaseUrl}${this.urlmethodById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(method: Method): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatemethod}`, { method }, this.authenticSvc.getHttpHeader());
  }

  add(method: Method): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddmethod}`, { method }, this.authenticSvc.getHttpHeader());
  }

  delete(methodId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletemethod}`, { id: methodId }, this.authenticSvc.getHttpHeader());
  }
}
