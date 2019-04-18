import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export class Pesticide {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  category: number;
  isUsed: boolean = true;

  public constructor(init?: Partial<Pesticide>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class PesticideService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlPesticideCategories: string = `${appUrl.apiPesticide}/gets`;
  private urlAllPesticideCategories: string = `${appUrl.apiPesticide}/gets/all`;
  private urlpesticideById: string = `${appUrl.apiPesticide}/get`;
  private urlAddpesticide: string = `${appUrl.apiPesticide}/insert`;
  private urlUpdatepesticide: string = `${appUrl.apiPesticide}/update`;
  private urlDeletepesticide: string = `${appUrl.apiPesticide}/delete`;

  gets(): Observable<ResultModel<Pesticide[]>> {
    return this.http.get<ResultModel<Pesticide[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPesticideCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<Pesticide[]>> {
    return this.http.get<ResultModel<Pesticide[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllPesticideCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Pesticide>> {
    return this.http.get<ResultModel<Pesticide>>(`${AppConsts.remoteServiceBaseUrl}${this.urlpesticideById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(pesticide: Pesticide): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatepesticide}`, { pesticide }, this.authenticSvc.getHttpHeader());
  }

  add(pesticide: Pesticide): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddpesticide}`, { pesticide }, this.authenticSvc.getHttpHeader());
  }

  delete(pesticideId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletepesticide}`, { id: pesticideId }, this.authenticSvc.getHttpHeader());
  }
}
